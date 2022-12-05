using BikingServerCache.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace BikingServerCache.Models
{
    public class GenericProxyCache<T> where T : ICacheItem, new()
    {

        private MemoryCache cache;

        public GenericProxyCache()
        {
            cache = new MemoryCache(nameof(T));
        }

        public T Get(string CacheItemName)
        {
            if (cache.Contains(CacheItemName))
            {
                return (T)cache.Get(CacheItemName);
            }
            return createCacheEntry(CacheItemName, ObjectCache.InfiniteAbsoluteExpiration);
           
        }

        public T Get(string CacheItemName, double dt_seconds)
        {
            if (!cache.Contains(CacheItemName))
            {
                return createCacheEntry(CacheItemName, DateTimeOffset.Now.AddSeconds(dt_seconds));
            }
            return (T)cache.Get(CacheItemName);
        }

        public T Get(string CacheItemName, DateTimeOffset dt)
        {

            if (!cache.Contains(CacheItemName))
            {
                return createCacheEntry(CacheItemName, dt);
            }
            return (T)cache.Get(CacheItemName);
        }

        private T createCacheEntry(string CacheItemName, DateTimeOffset expirationTime)
        {
            T obj = new T();
            obj.Init().Wait();
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = expirationTime;
            cache.Set(CacheItemName, obj, policy);
            return obj;
        }
    }
}
