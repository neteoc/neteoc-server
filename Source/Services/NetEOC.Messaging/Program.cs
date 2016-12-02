using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using NetEOC.Shared.Configuration;

namespace NetEOC.Messaging
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
#if RELEASE
                .UseUrls("http://*:" + ApplicationConfiguration.Configuration["system:release:port"])
#else
                .UseUrls("http://*:" + ApplicationConfiguration.Configuration["system:debug:port"])
#endif
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseConfiguration(ApplicationConfiguration.Configuration)
                .Build();
            host.Run();
        }
    }
}
