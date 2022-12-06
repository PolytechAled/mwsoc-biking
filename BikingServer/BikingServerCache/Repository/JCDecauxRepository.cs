using BikingServerCache.Models.JCDecaux;
using RestLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BikingServerCache.Repositories
{
    public class JCDecauxRepository
    {
        private RestClient restClient;

        public JCDecauxRepository()
        {
            restClient = new RestClient("https://api.jcdecaux.com");
            restClient.SetGetApiKey("apiKey", Program.JC_DECAUX_API_KEY);
        }

        public async Task<List<JC_Contract>> GetContracts()
        {
            var result = await restClient.GetRequest("vls/v3/contracts");
            return JsonSerializer.Deserialize<List<JC_Contract>>(result);
        }

        public async Task<List<JC_Station>> GetStations(string contract = "")
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(contract))
            {
                data.Add("contract", contract);
            }

            var result = await restClient.GetRequest("vls/v3/stations", data);
            return JsonSerializer.Deserialize<List<JC_Station>>(result);
        }

        public async Task<JC_Station> GetStationDetails(string contract, int stationId)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("contract", contract);

            var result = await restClient.GetRequest("vls/v3/stations/" + stationId, data);
            return JsonSerializer.Deserialize<JC_Station>(result);
        }
    }
}
