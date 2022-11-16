using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BikingServer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class BikingService : IBikingService
    {
        public void CalculatePath(double startLongitude, double startLatitude, double endLongitude, double endLatitude)
        {
            throw new NotImplementedException();
        }
    }
}
