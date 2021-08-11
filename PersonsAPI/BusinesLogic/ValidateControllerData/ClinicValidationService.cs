using BusinesLogic.Abstraction.Requests;
using FluentValidation;

namespace BusinesLogic.ValidateControllerData
{
    public class ClinicValidationService : AbstractValidator<ClinicToPost>
    {
        public ClinicValidationService()
        {
            RuleFor(x => x.Name)
                .Length(3,100)
                    .WithMessage("{PropertyName} длинна должна быть от трех до 100 символов")
                    .WithErrorCode("C-100.11");

            RuleFor(x => x.Adress)
                .Length(5, 150)
                    .WithMessage("{PropertyName} длинна должна быть от пяти до 150 символов")
                    .WithErrorCode("C-100.12");
        }
    }
}
