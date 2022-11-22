using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BikingServer.Models.Osm
{
    public class OSM_Route
    {
        [JsonPropertyName("segments")]
        public List<OSM_Segment> Segments { get; set; }

        [JsonPropertyName("geometry")]
        public string Geometry { get; set; }
    }
}
