using BusinesLogic.Abstraction.Services;
using BusinesLogic.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BusinesLogic
{
    public static class Registration
    {
        public static IServiceCollection RegisterBusinesLogic (this IServiceCollection services)
        {
            return services.AddTransient<IPersonService, PersonService>();
        }

        public static IServiceCollection RegisterBusinesLogicClinic(this IServiceCollection services)
        {
            return services.AddTransient<IClinicService, ClinicService>();
        }

    }
}
