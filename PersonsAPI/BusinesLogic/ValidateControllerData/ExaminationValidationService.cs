using BusinesLogic.Abstraction.Requests;
using FluentValidation;
using System;

namespace BusinesLogic.ValidateControllerData
{
    public class ExaminationValidationService : AbstractValidator<ExaminationToPost>
    {        
        public ExaminationValidationService()
        {
            RuleFor(x => x.ProcedureName)
                .Length(3, 100)
                    .WithMessage("{PropertyName} длинна должна быть от трех до 100 символов")
                    .WithErrorCode("E-100.01");

            RuleFor(x => x.ProcedureDate)
                .Must(date => date != default(DateTime))
                    .WithMessage("{PropertyName} Укажите действительную дату и время")
                    .WithErrorCode("E-100.02");

            RuleFor(x => x.ProcedureDate.ToString())
                .Must(IsValidDate)
                    .WithMessage("{PropertyName} Некорректный формат DateTime. Используйте такой формат: 2021-08-04T05:13:51.522Z")
                    .WithErrorCode("E-100.03");

            RuleFor(x => x.ProcedureCost)
                .GreaterThan(0)
                    .WithMessage("{PropertyName} должен быть больше нуля")
                    .WithErrorCode("E-100.4");

            RuleFor(x => x.ClinicId)
                .GreaterThan(0)
                    .WithMessage("{PropertyName} должен быть больше нуля")
                    .WithErrorCode("E-100.5");

            RuleFor(x => x.PersonId)
                .GreaterThan(0)
                    .WithMessage("{PropertyName} должен быть больше нуля")
                    .WithErrorCode("E-100.6");
        }

        private bool IsValidDate(string value)
        {
            DateTime date;
            return DateTime.TryParse(value, out date);
        }
    }
}
