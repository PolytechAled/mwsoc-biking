using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BikingServer.Models
{
    [DataContract]
    public enum NavigationError
    {
        [EnumMember]
        SUCCESS,
        [EnumMember]
        INTERNAL_ERROR,
        [EnumMember]
        NO_PATH_FOUND,
        [EnumMember]
        NO_LOCATION_FOUND,
    }
}
