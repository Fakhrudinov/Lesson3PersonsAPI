using BusinesLogic.Abstraction.DTO;
using FluentValidation;

namespace BusinesLogic.ValidateControllerData
{
    public sealed class ClinicIDValidationService : AbstractValidator<ClinicToGet>
    {
        public ClinicIDValidationService()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                    .WithMessage("{PropertyName} должен быть больше нуля")
                    .WithErrorCode("C-100.1");
        }
    }
}
