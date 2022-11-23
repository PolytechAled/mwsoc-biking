using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikingServerCache.Repository
{
    public class CacheRepository
    {

        public CacheRepository()
        {

        }

        public T Get<T>(string CacheItemName)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string CacheItemName, double dt_secondes)
        {
            throw new NotImplementedException();
        }   
        
        public T Get<T>(string CacheItemName, DateTimeOffset dt)
        {
            throw new NotImplementedException();
        }
    }
}
