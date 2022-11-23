using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BikingServer.Models.JCDecaux
{
    public class JC_StandDetails
    {
        [JsonPropertyName("bikes")]
        public int Capacity { get; set; }

        [JsonPropertyName("stands")]
        public int Stans { get; set; }

        [JsonPropertyName("mechanicalBikes")]
        public int MechanicalBikes { get; set; }

        [JsonPropertyName("electricalBikes")]
        public int ElectricalBikes { get; set; }

        [JsonPropertyName("electricalInternalBatteryBikes")]
        public int ElectricalInternalBatteryBikes { get; set; }

        [JsonPropertyName("electricalRemovableBatteryBikes")]
        public int ElectricalRemovableBatteryBikes { get; set; }

        [JsonIgnore]
        public int AvailableBike { get => MechanicalBikes+ElectricalBikes; }
    }
}
