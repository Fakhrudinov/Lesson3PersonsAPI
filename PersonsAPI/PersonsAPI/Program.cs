using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PersonsAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .ConfigureAppConfiguration((ctx, config) =>
                        {
                            config.AddEnvironmentVariables();
                        })
                        .ConfigureLogging(builder => builder.AddJsonConsole())
                        .UseStartup<Startup>();
                });
    }
}
