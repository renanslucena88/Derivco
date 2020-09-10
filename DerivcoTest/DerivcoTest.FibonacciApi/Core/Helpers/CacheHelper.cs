using Microsoft.Extensions.Caching.Memory;

namespace DerivcoTest.FibonacciApi.Core.Helpers
{
    public class CacheHelper
    {
        private readonly IMemoryCache _cache;

        public CacheHelper(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }
    }
}