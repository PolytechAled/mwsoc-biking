﻿using BikingServer.Models.JCDecaux;
using BikingServerCache.Cache;
using BikingServerCache.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BikingServerCache
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class BikingCache : IBikingCache
    {

        private GenericProxyCache<JCDecauxItem> cacheJC;
        
        public BikingCache()
        {
            cacheJC = new GenericProxyCache<JCDecauxItem>();
        }
        public List<JC_Station> GetJCStations()
        {
            return cacheJC.Get("jc",30).GetStations();
        }
    }
}
