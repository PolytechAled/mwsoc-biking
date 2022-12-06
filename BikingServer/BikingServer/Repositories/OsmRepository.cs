using BikingServer.BikingCache;
using BikingServer.Models;
using BikingServer.Models.Osm;
using RestLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace BikingServer.Repositories
{
    public class OsmRepository
    {
        private RestClient client;
        
        public OsmRepository()
        {
            client = new RestClient("https://api.openrouteservice.org");
            client.SetAuthorization("Authorization", Program.OPEN_ROUTE_API_KEY);
            client.SetGetApiKey("api_key", Program.OPEN_ROUTE_API_KEY);
        }

        public async Task<GeoCoordinate> GetPosition(string address)
        {
            try
            {
                Dictionary<string, string> param = new Dictionary<string, string>()
                {
                    { "text", address },
                };

                var jsonNode = await client.GetRequest("geocode/search", param);
                return new GeoCoordinate()
                {
                    Longitude = (double)jsonNode["features"][0]["geometry"]["coordinates"][0],
                    Latitude = (double)jsonNode["features"][0]["geometry"]["coordinates"][1]
                };
            }
            catch
            {
                return null;
            }
        }

        public async Task<OSM_Route> GetNavigation(GeoCoordinate start, GeoCoordinate end, bool isBicycle = false)
        {
            try
            {
                List<double[]> coords = new List<double[]>();
                coords.Add(new double[] { start.Longitude, start.Latitude });
                coords.Add(new double[] { end.Longitude, end.Latitude });

                Dictionary<string, object> param = new Dictionary<string, object>()
                {
                    {"coordinates", coords },
                    {"language","fr-fr" },
                    {"units","km" }
                };

                JsonNode jsonReturnInfo = await client.PostRequest("v2/directions/" + (isBicycle ? "cycling-regular" : "foot-walking") + "/json", param);
                return JsonSerializer.Deserialize<List<OSM_Route>>(jsonReturnInfo["routes"]).FirstOrDefault();
            }
            catch
            {
                Console.WriteLine("The path doesn't exist");
                return null;
            }
        }
    }
}
