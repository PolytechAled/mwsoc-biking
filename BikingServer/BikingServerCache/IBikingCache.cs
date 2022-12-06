using BikingServerCache.Models.JCDecaux;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BikingServerCache
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IBikingCache
    {
        [OperationContract]
        List<JC_Station> GetJCStations();

        // TODO: Add your service operations here
    }

}
