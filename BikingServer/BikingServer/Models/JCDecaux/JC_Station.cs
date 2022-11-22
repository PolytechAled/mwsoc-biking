using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BikingServer.Models.JCDecaux
{
    public class JC_Station
    {
        [JsonPropertyName("number")]
        public int Number { get; set; }

        [JsonPropertyName("contractName")]
        public string Contract { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("position")]
        public GeoCoordinate Position { get; set; }

        [JsonPropertyName("lastUpdate")]
        public string LastUpdate { get; set; }
    }
}
