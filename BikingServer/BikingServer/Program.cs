using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace BikingServer
{
    public class Program
    {
        public static string OPEN_ROUTE_API_KEY = "5b3ce3597851110001cf62486537b9afff0f4690ac90c9034c9116c5";

        static void Main(string[] args)
        {
            if (args != null && args.Length > 0)
            {
                OPEN_ROUTE_API_KEY = args[0];
            }

            Uri uri = new Uri("http://localhost:8733/BikingServer/Service");

            using (ServiceHost host = new ServiceHost(typeof(BikingService), uri))
            {
                host.AddServiceEndpoint(typeof(IBikingService), new BasicHttpBinding(), "");
                if (host.Description.Behaviors.Find<ServiceMetadataBehavior>() == null)
                {
                    ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
                    behavior.HttpGetEnabled = true;
                    behavior.HttpGetUrl = new Uri(uri + "/wsdl");
                    host.Description.Behaviors.Add(behavior);
                }
                host.Opened += delegate
                {
                    Console.WriteLine("BikingService is running！"); 
                };

                host.Open();
                Console.Read();
            }
        }
    }
}
