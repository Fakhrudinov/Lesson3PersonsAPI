using BusinesLogic.Abstraction.DTO;
using FluentValidation;

namespace BusinesLogic.ValidateControllerData
{
    public sealed class PersonIDValidationService : AbstractValidator<PersonToGet>
    {
        public PersonIDValidationService()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                    .WithMessage("{PropertyName} должен быть больше нуля")
                    .WithErrorCode("P-100.1");
        }
    }
}
