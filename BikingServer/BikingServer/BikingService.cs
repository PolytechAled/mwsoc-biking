﻿using BikingServer.Repositories;
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
        private JCDecauxRepository jcDecauxRepository;
        private string apiKeyOSM;
    
        public BikingService()
        {
            osmClient = new RestClient("https://api.openrouteservice.org");
            apiKeyOSM = "5b3ce3597851110001cf62486537b9afff0f4690ac90c9034c9116c5";
            osmClient.SetAuthorization("Authorization", apiKeyOSM);

            jcDecauxRepository = new JCDecauxRepository();
        }

        public async Task<string> CalculatePath(string startPoint, string endPoint)
        {
            Dictionary<string, string> paramStartPoint = new Dictionary<string, string>()
            {
                {"api_key", apiKeyOSM},
                { "text", startPoint },
            };

            Dictionary<string, string> paramEndPoint = new Dictionary<string, string>()
            {
                {"api_key", apiKeyOSM },
                { "text", endPoint },
            };

            JsonNode jsonStartPoint = await osmClient.GetRequest("/geocode/search", paramStartPoint);
            JsonNode jsonEndPoint = await osmClient.GetRequest("/geocode/search", paramEndPoint);

            string startCoordinates = GetParseCoordinates(jsonStartPoint)[0].ToString() + "," + GetParseCoordinates(jsonStartPoint)[1].ToString();

            string endCoordinates = GetParseCoordinates(jsonEndPoint)[0].ToString() + "," + GetParseCoordinates(jsonEndPoint)[1].ToString();

            return "["+startCoordinates+"],["+endCoordinates+"]"; 
        }

        private JsonNode GetParseCoordinates(JsonNode jsonNode)
        {
            return jsonNode["features"][0]["geometry"]["coordinates"];
        }

        public async Task<string> CalculateRoute(string startEndCoordinates)
        {
            List<double[]> coords = new List<double[]>();
            coords.Add(new double[] { 7.106732, 43.587719 });
            coords.Add(new double[] { 5.395393, 43.8782339 });

            Dictionary<string, object> param = new Dictionary<string, object>()
            {
                {"coordinates", coords },
                {"language","fr-fr" },
                {"units","km" }
            };

            JsonNode jsonReturnInfo = await osmClient.PostRequest("v2/directions/foot-walking/json", param);

            return jsonReturnInfo.ToString();
        }

        public async Task<string> Test()
        {
            var a = await jcDecauxRepository.GetStations();
            return "test";
        }
    }
}
