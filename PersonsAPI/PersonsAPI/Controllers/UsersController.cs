using BusinesLogic.Abstraction.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace PersonsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        /// Получение токенов ранее зарегистрированным пользователем. Требуется ввести логин и пароль
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromQuery] string login, string password)
        {
            string hashed = GetHashFromString(password);

            var token = await _userService.Authentificate(login, hashed);
            if (token is null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            SetTokenCookie(token.RefreshToken);
            return Ok(token);
        }

        /// <summary>
        /// Регистрация нового пользователя. Логин должен быть уникальным.
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterNew([FromQuery] string login, string password)
        {
            string hashed = GetHashFromString(password);

            int check = await _userService.GetUserByLogonAsync(login, hashed);
            if (check == 0)
            {               
                await _userService.CreateNewUserAsync(login, hashed);

                return Ok();
            }
            else
            {
                return BadRequest (new { message = "Error! User already exist." });
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
