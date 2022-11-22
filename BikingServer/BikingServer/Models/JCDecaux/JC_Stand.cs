using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BikingServer.Models.JCDecaux
{
    public class JC_Stand
    {
        [JsonPropertyName("availabilities")]
        public JC_StandDetails Details { get; set; }

        [JsonPropertyName("capacity")]
        public int Capacity { get; set; }
    }
}
