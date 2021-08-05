using BusinesLogic.Abstraction.Requests;
using FluentValidation;


namespace BusinesLogic.ValidateControllerData
{
    public class ClinicSearchWithPaginationValidationService : AbstractValidator<SearchWithPaginationRequest>
    {
        public ClinicSearchWithPaginationValidationService()
        {
            RuleFor(x => x.SearchTerm)
                .Length(2, 10)
                    .WithMessage("Количество символов для поиска должно быть от двух до 10")
                    .WithErrorCode("C-100.5");

            RuleFor(x => x.Skip)
                .GreaterThanOrEqualTo(0)
                    .WithMessage("{PropertyName} должен быть больше или равен нулю")
                    .WithErrorCode("C-100.6")
                .LessThanOrEqualTo(50)
                    .WithMessage("{PropertyName} должен быть меньше или равен 50")
                    .WithErrorCode("C-100.7");

            RuleFor(x => x.Take)
                .GreaterThan(0)
                    .WithMessage("{PropertyName} должен быть больше нуля")
                    .WithErrorCode("C-100.8")
                .LessThanOrEqualTo(100)
                    .WithMessage("{PropertyName} должен быть меньше или равен 100")
                    .WithErrorCode("C-100.9");
        }
    }
}
