using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DataBaseMigration
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) => Host
            .CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddDbContextFactory<ApplicationDataContext>(options =>
                {
                    options.UseNpgsql
                    (
                        hostContext.Configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(nameof(DataBaseMigration))
                    ).UseLoggerFactory(LoggerFactory.Create(builder =>
                    {
                        builder
                            .AddConsole((_) => { })
                            .AddFilter((category, level) =>
                                category == DbLoggerCategory.Database.Command.Name
                                && level == LogLevel.Information);
                    }));
                });               
                services.AddHostedService<Worker>();
            });
    }
}
