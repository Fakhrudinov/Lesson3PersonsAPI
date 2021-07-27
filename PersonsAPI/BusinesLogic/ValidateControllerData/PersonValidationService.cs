using FluentValidation;
using PersonsAPI.Requests;

namespace BusinesLogic.ValidateControllerData
{
    public class PersonValidationService : AbstractValidator<PersonToPost>
    {
        public PersonValidationService()
        {
            RuleFor(x => x.FirstName)
                .Length(3, 20)
                    .WithMessage("{PropertyName} длинна должна быть от трех до 20 символов")
                    .WithErrorCode("P-100.9")
                .Matches("^([А-Я]{1}[а-яё]{2,19}|[A-Z]{1}[a-z]{2,19})$")
                    .WithMessage("{PropertyName} должно начинаться с заглавной буквы, состоять только из символов одного алфавита и не содержать цифры")
                    .WithErrorCode("P-100.11");

            RuleFor(x => x.LastName)
                .Length(3, 20)
                    .WithMessage("{PropertyName} длинна должна быть от трех до 20 символов")
                    .WithErrorCode("P-100.9")
                .Matches("^([А-Я]{1}[а-яё]{2,19}|[A-Z]{1}[a-z]{2,19})$")
                    .WithMessage("{PropertyName} должно  начинаться с заглавной буквы, состоять только из символов одного алфавита и не содержать цифры")
                    .WithErrorCode("P-100.12");

            RuleFor(x => x.Email)
                .Length(5, 50)
                    .WithMessage("{PropertyName} длинна должна быть от пяти до 50 символов")
                    .WithErrorCode("C-100.10")
                .Matches("^[-\\w.]+@([A-z0-9][-A-z0-9]+\\.)+[A-z]{2,4}$")
                    .WithMessage("{PropertyName} некорректен")
                    .WithErrorCode("P-100.13");

            RuleFor(x => x.Age)
                .GreaterThan(0)
                    .WithMessage("{PropertyName} возраст должен быть больше ноля")
                    .WithErrorCode("C-100.14")
                .LessThanOrEqualTo(120)
                    .WithMessage("{PropertyName} возраст не может превышать 120 лет")
                    .WithErrorCode("C-100.15");

            // а компании может и не быть.
        }
    }
}
