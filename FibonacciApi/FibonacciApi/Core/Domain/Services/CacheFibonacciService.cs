using FibonacciApi.Core.Domain.Interfaces;
using System.Numerics;

namespace FibonacciApi.Core.Domain.Services
{
    public class CacheFibonacciService : ICacheFibonacciService
    {
        private readonly ICacheProvider _cacheProvider;

        public CacheFibonacciService(ICacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        public BigInteger[] GetFibonacciCached(int key)
        {
            return _cacheProvider.GetFromCache<BigInteger[]>(key);
        }

        public void SetFibonacciCache(int key, BigInteger[] value)
        {
            _cacheProvider.SetCache(key, value);
        }

        public void ClearCache(int key)
        {
            _cacheProvider.ClearCache(key);
        }
    }
}