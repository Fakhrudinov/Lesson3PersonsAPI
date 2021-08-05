using BusinesLogic.Abstraction.Requests;
using FluentValidation;

namespace BusinesLogic.ValidateControllerData
{
    public class JoinWithPaginationValidationService : AbstractValidator<IdWithPaginationRequest>
    {
        public JoinWithPaginationValidationService()
        {
            RuleFor(x => x.Skip)
                .GreaterThanOrEqualTo(0)
                    .WithMessage("{PropertyName} должен быть больше или равен нулю")
                    .WithErrorCode("J-100.4")
                .LessThanOrEqualTo(50)
                    .WithMessage("{PropertyName} должен быть меньше или равен 50")
                    .WithErrorCode("J-100.5");

            RuleFor(x => x.Take)
                .GreaterThan(0)
                    .WithMessage("{PropertyName} должен быть больше нуля")
                    .WithErrorCode("J-100.6")
                .LessThanOrEqualTo(100)
                    .WithMessage("{PropertyName} должен быть меньше или равен 100")
                    .WithErrorCode("J-100.7");

            RuleFor(x => x.Id)
                .GreaterThan(0)
                    .WithMessage("{PropertyName} должен быть больше или равен нулю")
                    .WithErrorCode("J-100.14");
        }
    }
}
