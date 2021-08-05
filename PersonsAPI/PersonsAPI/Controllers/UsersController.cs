using BusinesLogic.Abstraction.Services;
using BusinesLogic.ValidateControllerData;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonsAPI.Requests;
using PersonsAPI.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonsAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class UsersController : ControllerBase
    {
        // а где бы ее хранить?
        static byte[] salt = new byte[] { 122, 13, 208, 228, 66, 150, 15, 123, 15, 219, 47, 106, 22, 151, 5, 196 };

        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Создание нового пользователя с проверкой входных данных
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Create(NewUser user)
        {            
            var response = new ValidationResponseModel();
            
            UserToPost newUser = new UserToPost();
            newUser.Login = user.Login;
            newUser.Password = user.Password;

            // простые проверки
            UserValidationService validator = new UserValidationService();
            var validationResult = validator.Validate(newUser);

            if (!validationResult.IsValid)
            {
                response = SetResponseFromValidationResult(validationResult, response);

                return BadRequest(response);
            }

            // проверим логин на наличие в БД
            // https://docs.fluentvalidation.net/en/latest/async.html
            UserValidationAsyncService validatorAsync = new UserValidationAsyncService(_userService);
            var validationResultAsync = await validatorAsync.ValidateAsync(newUser);

            if (!validationResultAsync.IsValid)
            {
                response = SetResponseFromValidationResult(validationResultAsync, response);

                return UnprocessableEntity(response);
            }
            else
            {
                string hashed = GetHashFromString(newUser.Password);

                await _userService.CreateNewUserAsync(newUser.Login, hashed);
                return Ok(response);
            }
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(NewUser user)
        {
            var response = new ValidationResponseModel();

            UserToPost newUser = new UserToPost();
            newUser.Login = user.Login;
            newUser.Password = user.Password;

            // простые проверки
            UserValidationService validator = new UserValidationService();
            var validationResult = validator.Validate(newUser);

            if (!validationResult.IsValid)//false в случае наличия логина
            {
                response = SetResponseFromValidationResult(validationResult, response);

                return BadRequest(response);
            }

            // проверим логин на наличие в БД
            // https://docs.fluentvalidation.net/en/latest/async.html
            UserValidationAsyncService validatorAsync = new UserValidationAsyncService(_userService);
            var validationResultAsync = await validatorAsync.ValidateAsync(newUser);

            if (!validationResultAsync.IsValid)//false в случае наличия логина
            {
                string hashed = GetHashFromString(user.Password);

                var token = await _userService.Authentificate(user.Login, hashed);
                if (token is null)
                {
                    response.IsValid = false;
                    response.ValidationMessages.Add("L-200.4 Логин или пароль некорректны.");//здесь некорректен только пароль.
                                                                                             //Но надо ли так открыто говорить что одна часть подобрана правильно?
                    return BadRequest(response);
                }
                SetTokenCookie(token.RefreshToken);
                return Ok(token);
            }
            else
            {
                response.IsValid = false;
                response.ValidationMessages.Add("L-100.4 Логин или пароль некорректны.");//здесь некорректен логин. Пароль не проверялся.

                return BadRequest(response);
            }
        }


        /// <summary>
        /// Обновление токена. Доступно только зарегистрированным/аутентифицированным пользователям.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> Refresh()
        {
            string oldRefreshToken = Request.Cookies["refreshToken"];
            string newRefreshToken = await _userService.RefreshToken(oldRefreshToken);
            
            if (string.IsNullOrWhiteSpace(newRefreshToken))
            {
                return Unauthorized(new { message = "Invalid token" });
            }
            SetTokenCookie(newRefreshToken);

            return Ok(newRefreshToken);
        }

        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private ValidationResponseModel SetResponseFromValidationResult(ValidationResult validationResultAsync, ValidationResponseModel response)
        {
            List<string> ValidationMessages = new List<string>();

            response.IsValid = false;
            foreach (ValidationFailure failure in validationResultAsync.Errors)
            {
                ValidationMessages.Add(failure.ErrorCode + " " + failure.ErrorMessage);
            }
            response.ValidationMessages = ValidationMessages;

            return response;
        }

        private string GetHashFromString(string password)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10,
                numBytesRequested: 256 / 8));

            return hashed;
        }
    }
}