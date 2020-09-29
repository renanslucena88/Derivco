using FibonacciApi.Core.Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace FibonacciApi.Core.Infrasctructure
{
    public class CacheProvider : ICacheProvider
    {
        private readonly int CacheSeconds = Convert.ToInt32(Helpers.Config.GetValueFromKeyFromAppSetings("CacheOptions:CacheTime")); // 100 Seconds

        private readonly IMemoryCache _cache;

        public CacheProvider(IMemoryCache cache)
        {
            _cache = cache;
        }

        public T GetFromCache<T>(int key) where T : class
        {
            var cachedResponse = _cache.Get(key);
            return cachedResponse as T;
        }

        public void SetCache<T>(int key, T value) where T : class
        {
            SetCache(key, value, DateTimeOffset.Now.AddSeconds(CacheSeconds));
        }

        public void SetCache<T>(int key, T value, DateTimeOffset duration) where T : class
        {
            _cache.Set(key, value, duration);
        }

        public void ClearCache(int key)
        {
            _cache.Remove(key);
        }
    }
}