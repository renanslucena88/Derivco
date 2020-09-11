using DerivcoTest.FibonacciApi.Core.Domain.Interfaces;
using DerivcoTest.FibonacciApi.Core.Domain.Services;
using DerivcoTest.FibonacciApi.Core.Infrasctructure;
using Microsoft.Extensions.DependencyInjection;

namespace DerivcoTest.Test
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