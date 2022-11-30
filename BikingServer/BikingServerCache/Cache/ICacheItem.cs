using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikingServerCache.Cache
{
    public interface ICacheItem
    {

        Task Init();
    }
}
