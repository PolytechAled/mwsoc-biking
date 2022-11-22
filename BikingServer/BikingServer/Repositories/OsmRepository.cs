using BikingServer.Models;
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
                Latitude = (double)jsonNode["features"][0]["geometry"]["coordinates"][0],
                Longitude = (double)jsonNode["features"][0]["geometry"]["coordinates"][1]
            };
        }
    }
}
