using FibonacciApi.Core.Domain.Interfaces;
using FibonacciApi.Core.Domain.Services;
using FibonacciApi.Core.Infrasctructure;
using Microsoft.Extensions.DependencyInjection;

namespace FibonacciApi.Test
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services) =>
          services.AddScoped<IFibonacciService, FibonacciService>()
                    .AddScoped<ICacheFibonacciService, CacheFibonacciService>()
                    .AddScoped<ICacheProvider, CacheProvider>()
                    .AddMemoryCache();
    }
}