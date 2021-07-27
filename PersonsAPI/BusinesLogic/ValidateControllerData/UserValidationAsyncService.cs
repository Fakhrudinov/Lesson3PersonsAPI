using BusinesLogic.Abstraction.Services;
using FluentValidation;
using PersonsAPI.Requests;

namespace BusinesLogic.ValidateControllerData
{
    public class UserValidationAsyncService : AbstractValidator<UserToPost>
    {
        IUserService _userService;

        public UserValidationAsyncService(IUserService userService)
        {
            _userService = userService;

            RuleFor(x => x.Login).MustAsync(async (login, cancellation) => {
                int exists = await _userService.GetUserByLoginAsync(login);
                
                if (exists == 0)
                {
                    return true;
                }

                return false;
            }).WithMessage("{PropertyName} уже существует.")
            .WithErrorCode("L-100.3");
        }
    }
}
