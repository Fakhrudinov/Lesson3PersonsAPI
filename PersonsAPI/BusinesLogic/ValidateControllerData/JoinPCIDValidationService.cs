using BusinesLogic.Abstraction.Requests;
using FluentValidation;

namespace BusinesLogic.ValidateControllerData
{
    public class JoinPCIDValidationService : AbstractValidator<IdIdRequest>
    {
        public JoinPCIDValidationService()
        {
            RuleFor(x => x.ClinicId)
                .GreaterThan(0)
                    .WithMessage("{PropertyName} должен быть больше нуля")
                    .WithErrorCode("J-100.1");

            RuleFor(x => x.PersonId)
                .GreaterThan(0)
                    .WithMessage("{PropertyName} должен быть больше нуля")
                    .WithErrorCode("J-100.2");
        }
    }
}
