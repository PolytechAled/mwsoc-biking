using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BikingServer.Models
{
    [DataContract]
    public class NavigationStep
    {
        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public double Longitude { get; set; }

        [DataMember]
        public double Latitude { get; set; }
    }
}
