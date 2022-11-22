using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Web;
using static System.Collections.Specialized.BitVector32;

namespace RestLib
{
    public class RestClient
    {
        private readonly string service;
        private readonly HttpClient client;

        public RestClient(string service)
        {
            this.service = service;        
            this.client = new HttpClient();
        }

        public void SetApiKey(string name, string key)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(key);
        }

        public async Task<JsonNode> GetRequest(string endpoint, Dictionary<string, string> param)
        {
            try
            {
                var builder = new UriBuilder(service + "/" + endpoint);

                if (param != null && param.Any())
                {
                    var query = HttpUtility.ParseQueryString(builder.Query);
                    foreach (var kvp in param)
                    {
                        query.Add(kvp.Key, kvp.Value);
                    }
                    builder.Query = query.ToString();
                }

                HttpResponseMessage response = await client.GetAsync(builder.ToString());
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonNode.Parse(responseBody);
            }
            catch (HttpRequestException e)
            {
                throw e;
            }
        }

        public async Task<JsonNode> PostRequest(string endpoint, Dictionary<string,object> param)
        {
            try
            {
                StringContent data = new StringContent("", Encoding.UTF8, "application/json");
                var builder = new UriBuilder(service + "/" + endpoint);
                if(param != null && param.Any())
                {
                    var json = JsonSerializer.Serialize(param);
                    data = new StringContent(json, Encoding.UTF8, "application/json");
                }
                HttpResponseMessage response = await client.PostAsync(builder.ToString(),data);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonNode.Parse(responseBody);

            }
            catch (HttpRequestException e)
            {
                throw e;
            }
        }

    }
}
