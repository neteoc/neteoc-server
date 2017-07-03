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
                .AddJsonFile("hosting.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("conf.json", optional: true, reloadOnChange: true)
                .AddJsonFile("config.json", optional: true, reloadOnChange: true)
                .AddJsonFile("configs.json", optional: true, reloadOnChange: true)
                .AddJsonFile("configuration.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables(prefix: "NETEOC_")
                .Build();
            Configuration = config;
        }
    }
}
