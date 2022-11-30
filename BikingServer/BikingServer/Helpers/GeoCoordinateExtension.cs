using BikingServer.BikingCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikingServer.Helpers
{
    public static class GeoCoordinateExtension
    {

        public static double Distance(this GeoCoordinate self, GeoCoordinate other)
        {
            double d1 = (double)self.Latitude * (Math.PI / 180.0);
            double num1 = (double)self.Longitude * (Math.PI / 180.0);
            double d2 = (double)other.Latitude * (Math.PI / 180.0);
            double num2 = (double)other.Longitude * (Math.PI / 180.0) - num1;
            double d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);
            return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
        }

    }
}
