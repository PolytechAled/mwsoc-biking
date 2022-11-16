using RestLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace BikingServer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class BikingService : IBikingService
    {

        private RestClient osmClient;
    
        public BikingService()
        {
            osmClient = new RestClient("https://api.openrouteservice.org");
        }
        public async Task<string> CalculatePath(string startPoint, string endPoint)
        {
            Dictionary<string, string> param = new Dictionary<string, string>()
            {
                {"api_key", "5b3ce3597851110001cf62486537b9afff0f4690ac90c9034c9116c5"},
                { "text", startPoint },
            };

            JsonNode jsonStartPoint = await osmClient.GetRequest("/geocode/search", param);
            string startCoordinates = jsonStartPoint["features"][0]["geometry"]["coordinates"][0].ToString();
            return startCoordinates;
        }
    }
}
