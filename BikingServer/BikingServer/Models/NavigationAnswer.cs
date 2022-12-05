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
    public class NavigationAnswer
    {
        [DataMember]
        public NavigationError Error { get; set; }

        [DataMember(IsRequired = false)]
        public string ErrorDetails { get; set; }

        [DataMember]
        public string QueueName { get; set; }

        [DataMember]
        public bool UseBicycle { get; set; }

        [DataMember]
        public int StepCount { get; set; }

        [DataMember(IsRequired = false)]
        public List<InterestPoint> InterestPoints { get; set; }

    }
}
