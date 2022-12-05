﻿using BikingServer.BikingCache;
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

            try
            {
                bool useBicycle = true;

                var startPosition = await osmRepository.GetPosition(startPoint);

                var endPosition = await osmRepository.GetPosition(endPoint);

                var stationList = await bikingCache.GetJCStationsAsync();

                var nearestStartStationDistance = stationList.Where(s => s.Stand.Details.AvailableBike() > 0).Min(s => s.Position.Distance(startPosition));
                var nearestStartStation = stationList.Where(s => s.Position.Distance(startPosition).Equals(nearestStartStationDistance)).First();

                var nearestEndStationDistance = stationList.Min(s => s.Position.Distance(endPosition));
                var nearestEndStation = stationList.Where(s => s.Position.Distance(endPosition).Equals(nearestEndStationDistance)).First();

                if (nearestStartStation==nearestEndStation) useBicycle=false;

                var footPath = await osmRepository.GetNavigation(startPosition, endPosition, false);

                List<OSM_Route> cyclePath = new List<OSM_Route>();
                cyclePath.Add(await osmRepository.GetNavigation(startPosition, nearestStartStation.Position, false));
                cyclePath.Add(await osmRepository.GetNavigation(nearestStartStation.Position, nearestEndStation.Position, true));
                cyclePath.Add(await osmRepository.GetNavigation(nearestEndStation.Position, endPosition, false));

                double durationBicycle = cyclePath.Sum(s => s.Segments[0].Duration);

                if (footPath.Segments[0].Duration <= durationBicycle) useBicycle = false;


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

                answer.QueueName = activeMQRepository.GetRandomQueueName();
                answer.UseBicycle = useBicycle;
                answer.StepCount = steps.Count;
                foreach(var step in steps)
                {
                    activeMQRepository.SendMessageInQueue(answer.QueueName, step.Text);
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
