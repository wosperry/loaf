using Loaf.Admin.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace Loaf.Admin.Controllers.Login
{
    /// <summary>
    /// 登录
    /// </summary>
    [Route("login")]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userService"></param>
        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// 登录
        /// </summary>
        [HttpPost]
        public async Task<LoginResult> LoginAsync(LoginParameter input)
        {
            if (await _userService.CheckPasswordAsync(input.Account, input.Password))
            {
                return new LoginResult
                {
                    Success = true,
                    Token = "Fake TOken"
                };
            }
            else
            {
                return new LoginResult
                {
                    Success = false,
                    Message = "账号或密码不对，搞个记得住的账号密码不好吗？"
                };
            }
        }
    } 
}
