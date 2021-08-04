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

    }
}
