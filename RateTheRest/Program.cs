using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RateTheRest.Data;

namespace RateTheRest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Separate the build and run in order to create the db
            var host = CreateHostBuilder(args).Build();
            CreateDbIfNotExists(host);
            host.Run();
        }

        private static void CreateDbIfNotExists(IHost host)
        {
            //Get access to db contect which is a type of a service 
            using (var scope = host.Services.CreateScope())
            {
                //Create the service provider
                var services = scope.ServiceProvider;

                try
                {
                    //Create the dbcontext service provider
                    var context = services.GetRequiredService<ApplicationContext>();
                    //Create and initialize the db
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    //Create a logger to log an error
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Error with DB openning");
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
