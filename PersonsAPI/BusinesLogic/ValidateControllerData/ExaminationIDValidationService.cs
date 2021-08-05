using BusinesLogic.Abstraction.DTO;
using FluentValidation;

namespace BusinesLogic.ValidateControllerData
{
    public class ExaminationIDValidationService : AbstractValidator<ExaminationToGet>
    {
        public ExaminationIDValidationService()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                    .WithMessage("{PropertyName} должен быть больше нуля")
                    .WithErrorCode("E-100.7");
        }
    }
}
