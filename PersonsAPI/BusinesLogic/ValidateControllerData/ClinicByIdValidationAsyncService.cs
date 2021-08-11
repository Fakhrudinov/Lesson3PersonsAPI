using BusinesLogic.Abstraction.DTO;
using BusinesLogic.Abstraction.Services;
using FluentValidation;

namespace BusinesLogic.ValidateControllerData
{
    public class ClinicByIdValidationAsyncService : AbstractValidator<ClinicToGet>
    {
        IClinicService _clinicService;

        public ClinicByIdValidationAsyncService(IClinicService clinicService)
        {
            _clinicService = clinicService;

            RuleFor(x => x.Id).MustAsync(async (id, cancellation) =>
            {
                ClinicToGet exists = await _clinicService.GetClinicByIdAsync(id);

                if (exists.Id != id)//не найдено 
                {
                    return false;
                }

                return true;
            });
        }
    }
}
