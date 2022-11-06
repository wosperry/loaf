using Loaf.Core.Data.Dtos;

namespace Loaf.Admin.Services.Users.Dtos
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserDto: EntityDto<Guid>
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string? Account { get; set; }
    }
}