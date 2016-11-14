using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace NetEOC.Shared.Configuration
{
    public static class ApplicationConfiguration
    {
        public static IConfigurationRoot Configuration { get; private set; }

        static ApplicationConfiguration()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hosting.json", optional: true)
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("conf.json", optional: true)
                .AddJsonFile("config.json", optional: true)
                .AddJsonFile("configs.json", optional: true)
                .AddJsonFile("configuration.json", optional: true)
                .AddEnvironmentVariables(prefix: "NETEOC_")
                .Build();
            Configuration = config;
        }
    }
}
