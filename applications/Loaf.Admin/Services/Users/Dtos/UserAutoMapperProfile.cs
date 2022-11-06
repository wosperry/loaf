using AutoMapper;
using Loaf.Admin.Entities;

namespace Loaf.Admin.Services.Users.Dtos
{
    public class UserAutoMapperProfile:Profile
    {
        public UserAutoMapperProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
