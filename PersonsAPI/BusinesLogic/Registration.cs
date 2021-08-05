using BusinesLogic.Abstraction.Services;
using BusinesLogic.Services;
using Microsoft.Extensions.DependencyInjection;

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

        public static IServiceCollection RegisterBusinesLogicPersonToClinic(this IServiceCollection services)
        {
            return services.AddTransient<IPersonToClinicService, PersonToClinicService>();
        }

        public static IServiceCollection RegisterBusinesLogicExamination(this IServiceCollection services)
        {
            return services.AddTransient<IExaminationService, ExaminationService>();
        }

    }
}
