using DerivcoTest.FibonacciApi.Controllers;
using DerivcoTest.FibonacciApi.Core.Domain.Interfaces;
using DerivcoTest.FibonacciApi.Core.Domain.Services;
using DerivcoTest.FibonacciApi.Core.Infrasctructure;
using DerivcoTest.FibonacciApi.Core.Models.Request;
using DerivcoTest.FibonacciApi.Core.Models.Response;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DerivcoTest.Test.Builder
{
    public class FibonacciServiceRequestBuilder
    {
        private static readonly ICacheProvider _cacheProvider = new CacheProvider(GetMemoryCache());
        private static readonly ICacheFibonacciService _cache = new CacheFibonacciService(_cacheProvider);
        private static readonly IFibonacciService _service = new FibonacciService(_cache);
        private static readonly FibonacciRequest request = FibonacciRequest.Instance;

        public static IMemoryCache GetMemoryCache()
        {
            var memoryCacheOptions = new MemoryCacheOptions();
            return new MemoryCache(memoryCacheOptions);
        }

        public FibonacciRequest GetRequestInstance()
        {
            FibonacciRequest request = FibonacciRequest.Instance;
            request.FirstIndex = 28;
            request.LastIndex = 30;
            request.UseCache = false;
            request.TimeOfExecution = 10000;
            request.AmountMemory = 30000;
            return request;
        }

        public FibonacciServiceRequestBuilder New()
        {
            request.FirstIndex = 28;
            request.LastIndex = 30;
            request.UseCache = false;
            request.TimeOfExecution = 10000;
            request.AmountMemory = 30000;
            return new FibonacciServiceRequestBuilder();
        }

        public FibonacciServiceRequestBuilder ChangeFirstIndex(int firstIndex)
        {
            request.FirstIndex = firstIndex;
            return this;
        }

        public FibonacciServiceRequestBuilder ChangeLastIndex(int lastIndex)
        {
            request.LastIndex = lastIndex;
            return this;
        }

        public FibonacciServiceRequestBuilder ChangeUseCache(bool useCache)
        {
            request.UseCache = useCache;
            return this;
        }

        public FibonacciServiceRequestBuilder ChangeAmountMemory(int amountMemory)
        {
            request.AmountMemory = amountMemory;
            return this;
        }

        public FibonacciServiceRequestBuilder ChangeTimeOfExecution(int timeOfExecution)
        {
            request.TimeOfExecution = timeOfExecution;
            return this;
        }

        public async Task<BaseResponse> GetFibonacci()
        {
            return await _service.Fibonacci(request).ConfigureAwait(false);
        }

        public async Task<ErrorResponse> GetFibonacciErrorResponse()
        {
            return (ErrorResponse)await _service.Fibonacci(request).ConfigureAwait(false);
        }
    }
}