using BusinesLogic.Abstraction.Requests;
using FluentValidation;

namespace BusinesLogic.ValidateControllerData
{
    public class PersonSearchValidationService : AbstractValidator<SearchWithPaginationRequest>
    {
        public PersonSearchValidationService()
        {
            RuleFor(x => x.SearchTerm)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage("Количество символов для поиска не должно быть пустым")
                    .WithErrorCode("P-100.2")
                .NotNull()
                    .WithMessage("Количество символов для поиска не должно быть null")
                    .WithErrorCode("P-100.3")
                .Length(2, 10)
                    .WithMessage("Количество символов для поиска должно быть от двух до 10")
                    .WithErrorCode("P-100.4");
        }
    }
}
