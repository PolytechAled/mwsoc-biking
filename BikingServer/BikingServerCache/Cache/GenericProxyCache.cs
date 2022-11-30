using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace BikingServerCache.Models
{
    public class GenericProxyCache<T> : MemoryCache
    {

        public GenericProxyCache() : base(nameof(T))
        {

        }

        public T Get(string CacheItemName)
        {
            if (Contains(CacheItemName))
            {
                return Get(CacheItemName);
            }
            return createCacheEntry(CacheItemName, InfiniteAbsoluteExpiration);
           
        }

        public T Get(string CacheItemName, double dt_seconds)
        {
            if (!Contains(CacheItemName))
            {
                return createCacheEntry(CacheItemName, DateTimeOffset.Now.AddSeconds(dt_seconds));
            }
            return Get(CacheItemName);
        }

        public T Get(string CacheItemName, DateTimeOffset dt)
        {

            if (!Contains(CacheItemName))
            {
                return createCacheEntry(CacheItemName, dt);
            }
            return Get(CacheItemName);
        }

        private T createCacheEntry(string CacheItemName, DateTimeOffset expirationTime)
        {
            T obj = default;
            Set(CacheItemName, obj, expirationTime);
            return obj;
        }
    }
}
