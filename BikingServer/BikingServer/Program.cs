﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace BikingServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
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
