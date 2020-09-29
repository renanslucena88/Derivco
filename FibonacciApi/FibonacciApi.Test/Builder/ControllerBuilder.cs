using FibonacciApi.Controllers;
using FibonacciApi.Core.Domain.Interfaces;
using FibonacciApi.Core.Domain.Services;
using FibonacciApi.Core.Infrasctructure;
using FibonacciApi.Core.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FibonacciApi.Test.Builder
{
    public class ControllerBuilder
    {
        private static readonly ICacheProvider _cacheProvider = new CacheProvider(GetMemoryCache());
        private static readonly ICacheFibonacciService _cache = new CacheFibonacciService(_cacheProvider);
        private static readonly IFibonacciService _service = new FibonacciService(_cache);
        private static readonly FibonacciRequest request = new FibonacciRequest();

        public static IMemoryCache GetMemoryCache()
        {
            var memoryCacheOptions = new MemoryCacheOptions();
            return new MemoryCache(memoryCacheOptions);
        }
        public static ControllerBuilder New()
        {
            request.FirstIndex = 23;
            request.LastIndex = 30;
            request.UseCache = false;
            request.TimeOfExecution = 100;
            request.AmountMemory = 3000;
            return new ControllerBuilder();
        }

        public ControllerBuilder ChangeFirstIndex(int firstIndex)
        {
            request.FirstIndex = firstIndex;
            return this;
        }

        public ControllerBuilder ChangeLastIndex(int lastIndex)
        {
            request.LastIndex = lastIndex;
            return this;
        }

        public ControllerBuilder ChangeUseCache(bool useCache)
        {
            request.UseCache = useCache;
            return this;
        }

        public ControllerBuilder ChangeAmountMemory(int amountMemory)
        {
            request.AmountMemory = amountMemory;
            return this;
        }

        public ControllerBuilder ChangeTimeOfExecution(int timeOfExecution)
        {
            request.TimeOfExecution = timeOfExecution;
            return this;
        }

        public async Task<IActionResult> BuildToGetException()
        {
            FibonacciController _controller = new FibonacciController( _service);
            return await _controller.GetFibonacci(request).ConfigureAwait(false);
        }
    }
}