#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。
namespace Loaf.Admin.Services.Users.Dtos
{
    public class UserCreateDto
    {
        public string Name { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
    }
}
