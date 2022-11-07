using AutoMapper;
using Loaf.Admin.Entities;
using Loaf.Admin.Services.Users.Dtos;

namespace Loaf.Admin.Services.Users
{
    public class UserAutoMapperProfile : Profile
    {
        public UserAutoMapperProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
