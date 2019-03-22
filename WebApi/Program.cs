using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
        //    var host = new WebHostBuilder()
        //.UseContentRoot(Directory.GetCurrentDirectory())
        //.UseKestrel()
        //.UseIISIntegration()
        //.UseStartup<Startup>()
        //.ConfigureKestrel((context, options) =>
        //{


        //}).Build();

        //    host.Run();

             CreateWebHostBuilder(args).Build().Run();

        }

        //    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //WebHost.CreateDefaultBuilder(args)
        //    .UseStartup<Startup>()
        //    .ConfigureKestrel((context, options) =>
        //    {
        //        // Set properties and call methods on options
        //    });

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();

                })
                .UseStartup<Startup>();
    }
}
