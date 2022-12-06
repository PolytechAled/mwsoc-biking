using BikingServerCache.Cache;
using BikingServerCache.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace BikingServerCache
{
    public class Program
    {
        public static string JC_DECAUX_API_KEY = "802e12318759b6f39e5b2454f93bc29d081a44e7";
        public static GenericProxyCache<JCDecauxItem> JC_CACHE_INSTANCE = new GenericProxyCache<JCDecauxItem>();

        static void Main(string[] args)
        {
            if (args != null && args.Length > 0)
            {
                JC_DECAUX_API_KEY = args[0];
            }

            Uri uri = new Uri("http://localhost:8733/BikingCache/Service");

            using (ServiceHost host = new ServiceHost(typeof(BikingCache), uri))
            {
                host.AddServiceEndpoint(typeof(IBikingCache), new BasicHttpBinding(), "");
                if (host.Description.Behaviors.Find<ServiceMetadataBehavior>() == null)
                {
                    ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
                    behavior.HttpGetEnabled = true;
                    behavior.HttpGetUrl = new Uri(uri + "/wsdl");
                    host.Description.Behaviors.Add(behavior);
                }
                host.Opened += delegate
                {
                    Console.WriteLine("BikingCache is running！");
                };

                host.Open();
                Console.Read();
            }
        }
    }
}
