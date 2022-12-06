using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BikingServerCache.Models
{
    public class GeoCoordinate
    {

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }


        public double Distance(GeoCoordinate other)
        {
            double d1 = (double)Latitude * (Math.PI / 180.0);
            double num1 = (double)Longitude * (Math.PI / 180.0);
            double d2 = (double)other.Latitude * (Math.PI / 180.0);
            double num2 = (double)other.Longitude * (Math.PI / 180.0) - num1;
            double d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);
            return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
        }
    }
}
