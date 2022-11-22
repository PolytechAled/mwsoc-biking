using BikingServer.Repositories;
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

        private OsmRepository osmRepository;
        private JCDecauxRepository jcDecauxRepository;
    
        public BikingService()
        {
            jcDecauxRepository = new JCDecauxRepository();
            osmRepository = new OsmRepository();
        }

        public async Task<string> CalculatePath(string startPoint, string endPoint)
        {

            var a = await osmRepository.GetPosition(startPoint);
            var b = await osmRepository.GetPosition(endPoint);
            var c = await osmRepository.GetNavigation(a, b);
            return c;
        }
    }
}
