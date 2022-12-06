using BikingServerCache.Models.JCDecaux;
using BikingServerCache.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BikingServerCache.Cache
{
    public class JCDecauxItem : ICacheItem
    {

        private JCDecauxRepository jCDecauxRepository;
        private List<JC_Station> stations;

        public JCDecauxItem()
        {
            jCDecauxRepository = new JCDecauxRepository();
        }

        public async Task Init()
        {
            
            stations = await jCDecauxRepository.GetStations();
            Console.WriteLine("Reloading cache");
        }

        public List<JC_Station> GetStations()
        {
            return stations;
        }
    }
}
