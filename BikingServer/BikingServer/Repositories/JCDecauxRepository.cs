using BikingServer.Models.JCDecaux;
using RestLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BikingServer.Repositories
{
    public class JCDecauxRepository
    {
        private RestClient restClient;
        private const string apiKey = "802e12318759b6f39e5b2454f93bc29d081a44e7";

        public JCDecauxRepository()
        {
            restClient = new RestClient("https://api.jcdecaux.com");
            restClient.SetGetApiKey("apiKey", apiKey);
        }

        public async Task<List<JC_Contract>> GetContracts()
        {
            var result = await restClient.GetRequest("vls/v3/contracts");
            return JsonSerializer.Deserialize<List<JC_Contract>>(result);
        }
    }
}
