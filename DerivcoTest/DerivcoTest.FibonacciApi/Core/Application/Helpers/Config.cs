using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DerivcoTest.FibonacciApi.Core.Helpers
{
    public static class Config
    {
        public static IConfigurationRoot Configuration { get; set; }
        public static string Value { get; private set; }

        public static string GetValueFromKeyFromAppSetings(string key)
        {
            var builder = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .AddEnvironmentVariables();
            Configuration = builder.Build();
            Value = Configuration[key];
            return Value;
        }
    }
}
