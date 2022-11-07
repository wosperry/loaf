﻿using Loaf.Admin.Services.Users.Dtos;

namespace Loaf.Admin.Services.Users
{
    /// <summary>
    /// 
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// 校验用户密码
        /// </summary>
        /// <returns>True：校验通过</returns>
        Task<bool> CheckPasswordAsync(string username, string password);

        /// <summary>
        /// 创建用户
        /// </summary>
        Task<UserDto> CreateAsync(UserCreateDto input);

        /// <summary>
        /// 获取用户信息
        /// </summary>
        Task<UserDto> GetAsync(Guid id);
    }
}