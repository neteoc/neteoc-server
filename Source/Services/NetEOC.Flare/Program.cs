using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using NetEOC.Shared.Configuration;

namespace NetEOC.Flare
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
#if RELEASE
                .UseUrls("http://*:80")
#else
                .UseUrls()
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
