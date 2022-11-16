using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
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
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(name, key);
        }

        public async Task<T> GetRequest<T>(string endpoint, Dictionary<string, string> param)
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
                return JsonSerializer.Deserialize<T>(responseBody);
            }
            catch (HttpRequestException e)
            {
                throw e;
            }
        }

    }
}
