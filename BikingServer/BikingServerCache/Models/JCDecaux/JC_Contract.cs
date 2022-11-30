using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BikingServer.Models.JCDecaux
{
    public class JC_Contract
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("commercial_name")]
        public string CommercialName { get; set; }
        
        [JsonPropertyName("cities")]
        public List<string> Cities { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }
    }
}
