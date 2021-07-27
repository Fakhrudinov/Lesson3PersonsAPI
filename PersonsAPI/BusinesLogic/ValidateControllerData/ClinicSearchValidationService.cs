using BusinesLogic.Abstraction.Requests;
using FluentValidation;


namespace BusinesLogic.ValidateControllerData
{
    public class ClinicSearchValidationService : AbstractValidator<SearchWithPaginationRequest>
    {
        public ClinicSearchValidationService()
        {
            RuleFor(x => x.SearchTerm)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .WithMessage("Количество символов для поиска не должно быть пустым")
                    .WithErrorCode("C-100.2")
                .NotNull()
                    .WithMessage("Количество символов для поиска не должно быть null")
                    .WithErrorCode("C-100.3")
                .Length(2, 10)
                    .WithMessage("Количество символов для поиска должно быть от двух до 10")
                    .WithErrorCode("C-100.4");
        }
    }
}
