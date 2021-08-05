using FluentValidation;

namespace BusinesLogic.ValidateControllerData
{
    public class ExaminationBoolValidationService : AbstractValidator<bool>
    {
        public ExaminationBoolValidationService()
        {
            RuleFor(x => x)
                .NotNull()
                    .WithMessage("{PropertyName} должен быть передан")
                    .WithErrorCode("E-100.8");

            RuleFor(x => x.ToString())
                .Must(IsValidBool)
                    .WithMessage("{PropertyName} Некорректный формат поля. Используйте только значения true или false")
                    .WithErrorCode("E-100.9");
        }

        private bool IsValidBool(string value)
        {
            bool check;
            return bool.TryParse(value, out check);
        }
    }
}
