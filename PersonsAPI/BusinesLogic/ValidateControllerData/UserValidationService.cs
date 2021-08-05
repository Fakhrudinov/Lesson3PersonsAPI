using FluentValidation;
using PersonsAPI.Requests;


namespace BusinesLogic.ValidateControllerData
{
    public sealed class UserValidationService : AbstractValidator<UserToPost>
    {
        public UserValidationService()
        {
            RuleFor(x => x.Login)
                //.Cascade(CascadeMode.StopOnFirstFailure)// останавливать проверку после первой же ошибки
                .NotEmpty()
                    .WithMessage("{PropertyName} не должен быть пустым")
                    .WithErrorCode("L-100.1")
                .Length(4, 30)
                    .WithMessage("{PropertyName} должен быть от 4 до 30 символов")
                    .WithErrorCode("L-100.2");

            RuleFor(x => x.Password)
                .NotEmpty()
                    .WithMessage("{PropertyName} не должен быть пустым")
                    .WithErrorCode("L-200.1")
                .Length(8, 30)
                    .WithMessage("{PropertyName} должен быть от 8 до 30 символов")
                    .WithErrorCode("L-200.2")
                .Matches("(^(?=[^А-Яа-я\\s])((?=.*[!@#$%^&*()\\-_=+{};:,<.>]){1})" +
                        "(?=[^А-Яа-я\\s]+\\d|\\d)((?=[^А-Яа-я\\s]+[a-z]|[a-z]){1})" +
                        "((?=[^А-Яа-я\\s]+[A-Z]|[A-Z]){1})[^А-Яа-я\\s]+$)")
                    .WithMessage("{PropertyName} должен содержать как минимум 1 Заглавную, 1 строчную, 1 цифру, 1 спецсимвол")
                    .WithErrorCode("L-200.3");//основа здесь https://regex101.com/r/PNWxik/1
        }
    }
}
