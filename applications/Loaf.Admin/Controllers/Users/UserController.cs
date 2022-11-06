using Loaf.Admin.Entities;
using Loaf.Admin.Services.Users;
using Loaf.Admin.Services.Users.Dtos;
using Loaf.Core.Encryptors;
using Microsoft.AspNetCore.Mvc;

namespace Loaf.Admin.Controllers.Users
{
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        /// <summary>
        /// 创建用户
        /// </summary>
        [HttpPost] public Task<UserDto> CreateAsync(UserCreateDto input) => _userService.CreateAsync(input);

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns>用户DTO</returns>
        [HttpGet("{id}")] public Task<UserDto> GetAsync(Guid id) => _userService.GetAsync(id);
    }
}
