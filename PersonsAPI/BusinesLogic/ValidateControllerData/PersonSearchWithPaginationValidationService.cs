using BusinesLogic.Abstraction.Requests;
using FluentValidation;


namespace BusinesLogic.ValidateControllerData
{
    public class PersonSearchWithPaginationValidationService : AbstractValidator<SearchWithPaginationRequest>
    {
        public PersonSearchWithPaginationValidationService()
        {
            RuleFor(x => x.SearchTerm)
                .Length(2, 10)
                    .WithMessage("Количество символов для поиска должно быть от двух до 10")
                    .WithErrorCode("P-100.5");

            RuleFor(x => x.Skip)
                .GreaterThanOrEqualTo(0)
                    .WithMessage("{PropertyName} должен быть больше или равен нулю")
                    .WithErrorCode("P-100.6")
                .LessThanOrEqualTo(50)
                    .WithMessage("{PropertyName} должен быть меньше или равен 50")
                    .WithErrorCode("P-100.7");

            RuleFor(x => x.Take)
                .GreaterThan(0)
                    .WithMessage("{PropertyName} должен быть больше нуля")
                    .WithErrorCode("P-100.8")
                .LessThanOrEqualTo(100)
                    .WithMessage("{PropertyName} должен быть меньше или равен 100")
                    .WithErrorCode("P-100.9");
        }
    }
}
