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
        private const string apiKey = "5b3ce3597851110001cf62486537b9afff0f4690ac90c9034c9116c5";

        public OsmRepository()
        {
            client = new RestClient("https://api.openrouteservice.org");
            client.SetAuthorization("Authorization", apiKey);
            client.SetGetApiKey("api_key", apiKey);
        }

        public async Task<GeoCoordinate> GetPosition(string address)
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

        public async Task<OSM_Route> GetNavigation(GeoCoordinate start, GeoCoordinate end, bool isBicycle = false)
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

            JsonNode jsonReturnInfo = await client.PostRequest("v2/directions/"+ (isBicycle? "cycling-regular" : "foot-walking") +"/json", param);
            return JsonSerializer.Deserialize<List<OSM_Route>>(jsonReturnInfo["routes"]).FirstOrDefault();
        }
    }
}
