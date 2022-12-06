using BikingServer.BikingCache;
using BikingServer.Helpers;
using BikingServer.Models;
using BikingServer.Models.Osm;
using BikingServer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikingServer
{
    public class BikingService : IBikingService
    {

        private OsmRepository osmRepository;
        private BikingCacheClient bikingCache;
        private ActiveMQRepository activeMQRepository;
    
        public BikingService()
        {
            bikingCache = new BikingCacheClient();
            osmRepository = new OsmRepository();
            activeMQRepository = new ActiveMQRepository();
        }

        public async Task<NavigationAnswer> CalculatePath(string startPoint, string endPoint)
        {
            NavigationAnswer answer = new NavigationAnswer
            {
                Error = NavigationError.SUCCESS,
                InterestPoints = new List<InterestPoint>()
            };

            bool noPathFound = false;

            try
            {
                bool useBicycle = true;
                GeoCoordinate startPosition = await osmRepository.GetPosition(startPoint);
                if (startPosition == null)
                {
                    answer.Error = NavigationError.NO_LOCATION_FOUND;
                    answer.ErrorDetails = "The starting point was not recognized";
                    return answer;
                }
                else
                {
                    answer.InterestPoints.Add(new InterestPoint() { Latitude = startPosition.Latitude, Longitude = startPosition.Longitude, IsStand = false });
                }

                GeoCoordinate endPosition = await osmRepository.GetPosition(endPoint); ;
                if (endPosition == null)
                {
                    answer.Error = NavigationError.NO_LOCATION_FOUND;
                    answer.ErrorDetails = "The ending point was not recognized";
                    return answer;
                }


                // Get the bicycle stations from proxy cache
                var stationList = await bikingCache.GetJCStationsAsync();
                if (stationList != null && stationList.Length > 0) useBicycle = false;


                List<OSM_Route> cyclePath = new List<OSM_Route>();
                OSM_Route footPath = await osmRepository.GetNavigation(startPosition, endPosition, false);

                if (useBicycle)
                {
                    // Get the nearest stations to the start and end point
                    var nearestStartStationDistance = stationList.Where(s => s.Stand.Details.AvailableBike() > 0).Min(s => s.Position.Distance(startPosition));
                    var nearestStartStation = stationList.Where(s => s.Position.Distance(startPosition).Equals(nearestStartStationDistance)).First();
                    var nearestEndStationDistance = stationList.Min(s => s.Position.Distance(endPosition));
                    var nearestEndStation = stationList.Where(s => s.Position.Distance(endPosition).Equals(nearestEndStationDistance)).First();

                    // Check if the start station if the last station
                    if (nearestStartStation == nearestEndStation)
                    {
                        useBicycle = false;
                    }
                    else if (useBicycle)
                    {
                        answer.InterestPoints.Add(new InterestPoint() { Latitude = nearestStartStation.Position.Latitude, Longitude = nearestStartStation.Position.Longitude, IsStand = true });
                        answer.InterestPoints.Add(new InterestPoint() { Latitude = nearestEndStation.Position.Latitude, Longitude = nearestEndStation.Position.Longitude, IsStand = true });
                    }

                    // Calculate path with bicycle
                    try
                    {
                        cyclePath.Add(await osmRepository.GetNavigation(startPosition, nearestStartStation.Position, false));
                        cyclePath.Add(await osmRepository.GetNavigation(nearestStartStation.Position, nearestEndStation.Position, true));
                        cyclePath.Add(await osmRepository.GetNavigation(nearestEndStation.Position, endPosition, false));
                    }
                    catch
                    {
                        Console.WriteLine("The bicycle path doesn't exist");
                        noPathFound = true;
                    }

                    if (cyclePath.Count == 0)
                    {
                        noPathFound = true;
                    }


                    // Check which is faster between foot and bicycle
                    if (!noPathFound)
                    {
                        double durationBicycle = cyclePath.Sum(s => s.Segments[0].Duration);
                        if (footPath.Segments[0].Duration <= durationBicycle) useBicycle = false;
                    }
                }

                // If no path found return an error
                if ((footPath == null && noPathFound) || (!useBicycle && footPath == null))
                {
                    answer.Error = NavigationError.NO_PATH_FOUND;
                    answer.ErrorDetails = "No path exist with pedestrian or bicycle";
                    return answer;
                }

                answer.InterestPoints.Add(new InterestPoint() { Latitude = endPosition.Latitude, Longitude = endPosition.Longitude, IsStand = false });

                // Determine steps
                List<NavigationStep> steps = new List<NavigationStep>();
                if (useBicycle)
                {
                    steps = cyclePath.Select(s => s.Segments[0].Steps.Select(p => new NavigationStep()
                    {
                        Text = p.Instruction,
                        Latitude = p.WayPoints[0],
                        Longitude = p.WayPoints[1]
                    }).ToList()).Aggregate((s1, s2) =>
                    {
                        s1.AddRange(s2);
                        return s1;
                    }).ToList();

                }
                else
                {
                    steps = footPath.Segments[0].Steps.Select(s => new NavigationStep()
                    {
                        Text = s.Instruction,
                        Latitude = s.WayPoints[0],
                        Longitude = s.WayPoints[1]
                    }).ToList();
                }

                // Fill answer infos
                answer.QueueName = activeMQRepository.GetRandomQueueName();
                answer.UseBicycle = useBicycle;
                answer.StepCount = steps.Count;

                // Push step to activemq
                foreach(var step in steps)
                {
                    if (!activeMQRepository.SendMessageInQueue(answer.QueueName, step.Text))
                    {
                        answer.Error = NavigationError.INTERNAL_ERROR;
                        answer.ErrorDetails = "Error with activemq";
                        break;
                    }
                }
            }
            catch(Exception ex)
            {
                answer.Error = NavigationError.INTERNAL_ERROR;
                answer.ErrorDetails = ex.Message;
            }

            return answer;
        }
    }
}
