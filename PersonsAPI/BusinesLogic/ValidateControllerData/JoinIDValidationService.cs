using BusinesLogic.Abstraction.DTO;
using FluentValidation;


namespace BusinesLogic.ValidateControllerData
{
    public class JoinIDValidationService : AbstractValidator<PersonToGet>
    {
        public JoinIDValidationService()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                    .WithMessage("{PropertyName} должен быть больше нуля")
                    .WithErrorCode("J-100.3");
        }
    }
}
