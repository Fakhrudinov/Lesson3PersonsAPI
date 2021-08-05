using FluentValidation;
using System;


namespace BusinesLogic.ValidateControllerData
{
    public class ExaminationDateValidationService : AbstractValidator<DateTime>
    {
        public ExaminationDateValidationService()
        {
          
            RuleFor(x => x)
                .NotNull()
                    .WithMessage("{PropertyName} должна быть передана")
                    .WithErrorCode("E-100.10")
                .Must(date => date != default(DateTime))
                    .WithMessage("{PropertyName} - Укажите действительную дату и время")
                    .WithErrorCode("E-100.11");

            RuleFor(x => x.ToString())
                .Must(IsValidDate)
                    .WithMessage("{PropertyName} Некорректный формат DateTime. Используйте такой формат: 2021-08-04T05:13:51.522Z")
                    .WithErrorCode("E-100.12");          
        }

        private bool IsValidDate(string value)
        {
            DateTime date;
            return DateTime.TryParse(value, out date);
        }
    }
}
