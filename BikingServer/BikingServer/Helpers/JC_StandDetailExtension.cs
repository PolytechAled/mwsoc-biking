using BikingServer.BikingCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikingServer.Helpers
{
    public static class JC_StandDetailExtension
    {

        public static int AvailableBike(this JC_StandDetails jC_StandDetails)
        {
            return jC_StandDetails.ElectricalBikes + jC_StandDetails.MechanicalBikes;
        }

    }
}
