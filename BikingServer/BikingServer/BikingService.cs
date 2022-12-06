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
                Error = NavigationError.SUCCESS
            };

            bool noPathFound = false;

            try
            {
                bool useBicycle = true;
                GeoCoordinate startPosition = null;
                GeoCoordinate endPosition = null ;

                try
                {
                    // Get position for user start address
                    startPosition = await osmRepository.GetPosition(startPoint);
                    if(startPosition == null)
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    answer.Error = NavigationError.NO_LOCATION_FOUND;
                    answer.ErrorDetails = "The starting point was not recognized";
                    return answer;
                }

                try
                {
                    endPosition = await osmRepository.GetPosition(endPoint);
                    if(endPosition == null)
                    {
                        throw new Exception();
                    }
                }
                // Get position for user end address
                catch
                {
                    answer.Error = NavigationError.NO_LOCATION_FOUND;
                    answer.ErrorDetails = "The end point was not recognized";
                    return answer;
                }

                 // Get the bicycle stations from proxy cache
                var stationList = await bikingCache.GetJCStationsAsync();

                // Get the nearest stations to the start and end point
                var nearestStartStationDistance = stationList.Where(s => s.Stand.Details.AvailableBike() > 0).Min(s => s.Position.Distance(startPosition));
                var nearestStartStation = stationList.Where(s => s.Position.Distance(startPosition).Equals(nearestStartStationDistance)).First();
                var nearestEndStationDistance = stationList.Min(s => s.Position.Distance(endPosition));
                var nearestEndStation = stationList.Where(s => s.Position.Distance(endPosition).Equals(nearestEndStationDistance)).First();

                // Check if the start station if the last station
                if (nearestStartStation==nearestEndStation) useBicycle=false;

                OSM_Route footPath = null;
                try
                {
                    footPath = await osmRepository.GetNavigation(startPosition, endPosition, false);
                }
                catch
                {
                    Console.WriteLine("The foot path doesn't exist");
                }


                // Calculate path with bicycle
                List<OSM_Route> cyclePath = null;
                try
                {
                    cyclePath = new List<OSM_Route>();
                    cyclePath.Add(await osmRepository.GetNavigation(startPosition, nearestStartStation.Position, false));
                    cyclePath.Add(await osmRepository.GetNavigation(nearestStartStation.Position, nearestEndStation.Position, true));
                    cyclePath.Add(await osmRepository.GetNavigation(nearestEndStation.Position, endPosition, false));
                }
                catch
                {
                    Console.WriteLine("The bicycle path doesn't exist");
                    noPathFound = true;
                }
                
                if(footPath == null && noPathFound)
                {
                    answer.Error = NavigationError.NO_PATH_FOUND;
                    answer.ErrorDetails = "No path exist with pedestrian or bicycle";
                    return answer;
                }

                double durationBicycle = cyclePath.Sum(s => s.Segments[0].Duration);

                // Check which is faster between foot and bicycle
                if (footPath.Segments[0].Duration <= durationBicycle) useBicycle = false;

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

                // Interest point for answer
                answer.InterestPoints = new List<InterestPoint> {
                    new InterestPoint() { Latitude = startPosition.Latitude, Longitude = startPosition.Longitude, IsStand = false },
                };
                if (useBicycle)
                {
                    answer.InterestPoints.Add(new InterestPoint() { Latitude = nearestStartStation.Position.Latitude, Longitude = nearestStartStation.Position.Longitude, IsStand = true });
                    answer.InterestPoints.Add(new InterestPoint() { Latitude = nearestEndStation.Position.Latitude, Longitude = nearestEndStation.Position.Longitude, IsStand = true });
                }
                answer.InterestPoints.Add(new InterestPoint() { Latitude = endPosition.Latitude, Longitude = endPosition.Longitude, IsStand = false });
                
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
