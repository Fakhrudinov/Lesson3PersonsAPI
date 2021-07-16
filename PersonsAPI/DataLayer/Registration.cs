using DataLayer.Abstraction.Repositories;
using DataLayer.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DataLayer
{
    public static class Registration
    {
        public static IServiceCollection RegisterDataLayer(this IServiceCollection services)
        {
            return services.AddTransient<IPersonRepository, PersonRepository>();
        }

        public static IServiceCollection RegisterDataLayerClinic(this IServiceCollection services)
        {
            return services.AddTransient<IClinicRepository, ClinicRepository>();
        }
    }
}
