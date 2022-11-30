using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BikingServer.Models.JCDecaux
{
    [DataContract]
    public class JC_Station
    {
        [DataMember]
        [JsonPropertyName("number")]
        public int Number { get; set; }

        [DataMember]
        [JsonPropertyName("contractName")]
        public string Contract { get; set; }

        [DataMember]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [DataMember]
        [JsonPropertyName("address")]
        public string Address { get; set; }

        [DataMember]
        [JsonPropertyName("position")]
        public GeoCoordinate Position { get; set; }

        [DataMember]
        [JsonPropertyName("lastUpdate")]
        public string LastUpdate { get; set; }

        [DataMember]
        [JsonPropertyName("totalStands")]
        public JC_Stand Stand { get; set; }

        [DataMember]
        [JsonPropertyName("status")]
        public string StatusStr { get; set; }

        [JsonIgnore]
        public JC_Status Status
        {
            get
            {
                JC_Status status;
                Enum.TryParse<JC_Status>(StatusStr, out status);
                return status;
            }
        }
    }
}
