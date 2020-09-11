using DerivcoTest.FibonacciApi.Core.Config;
using DerivcoTest.FibonacciApi.Core.Domain.Interfaces;
using DerivcoTest.FibonacciApi.Core.Domain.Services;
using DerivcoTest.FibonacciApi.Core.Infrasctructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Reflection;

namespace DerivcoTest.FibonacciApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMemoryCache();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Swagger Doc for Derivco Fibonacci API",
                    Description = "Some examples of how works the Swagger Doc for Derivco Fibonacci API",
                    Version = "V1"
                });
            });

            //Dependency Injection
            services.AddScoped<IFibonacciService, FibonacciService>();
            services.AddScoped<ICacheFibonacciService, CacheFibonacciService>();
            services.AddScoped<ICacheProvider, CacheProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());

            #region Swagger Configuration

            var swaggerOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);
            app.UseSwagger(option => option.RouteTemplate = swaggerOptions.JsonRoute);
            app.UseSwaggerUI(option => option.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description));

            #endregion
        }
    }
}