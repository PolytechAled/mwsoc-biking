using BikingServer.BikingCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BikingServer.Models
{
    [DataContract]
    public class InterestPoint : GeoCoordinate
    {
        [DataMember]
        public string Description { get; set; }
    }
}
