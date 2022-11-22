using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BikingServer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IBikingService
    {

        // TODO: Add your service operations here
        [OperationContract]
        Task<string> CalculatePath(string startPoint, string endPoint);

        [OperationContract]
        Task<string> CalculateRoute(string StartEndCoordinates);

        [OperationContract]
        Task<string> Test();
    }

}
