using BusinesLogic.Abstraction.DTO;
using BusinesLogic.Abstraction.Requests;
using BusinesLogic.Abstraction.Services;
using FluentValidation;
using System.Collections.Generic;


namespace BusinesLogic.ValidateControllerData
{
    public class ClinicValidationAsyncService : AbstractValidator<ClinicToPost>
    {
        IClinicService _clinicService;

        public ClinicValidationAsyncService(IClinicService clinicService)
        {
            _clinicService = clinicService;


            RuleFor(x => x.Name).MustAsync(async (name, cancellation) => {
                IEnumerable <ClinicToGet> clinics = await _clinicService.GetClinicsByNameAsync(name);

                bool exists = false;

                foreach (ClinicToGet clinicSingle in clinics)
                {
                    if (clinicSingle.Name.Equals(name))
                    {
                        exists = true;
                        break;
                    }
                }

                if (!exists)
                {
                    return true;
                }

                return false;
            }).WithMessage("{PropertyName} - Клиника с таким именем уже существует.")
            .WithErrorCode("C-100.10");
        }
    }
}
