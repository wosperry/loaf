using Loaf.Core.Data;

namespace Loaf.Admin.Entities
{
    public class User:Entity<Guid>
    {
        public User(string name, string account, string password, string salt)
        {
            Name = name;
            Account = account;
            Password = password;
            Salt = salt;
        }

    
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 盐
        /// </summary>
        public string Salt { get; set; }
    }
}
