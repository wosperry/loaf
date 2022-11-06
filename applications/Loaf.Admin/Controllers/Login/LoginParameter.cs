#pragma warning disable CS8618

namespace Loaf.Admin.Controllers.Login
{
    /// <summary>
    /// 登录参数
    /// </summary>
    public class LoginParameter
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; internal set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; internal set; }
    }
}
