using Loaf.Core.Data;
using Loaf.EntityFrameworkCore.Tests.EFCore.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication12.Controllers
{
    [Route("user")] 
    
    public class UserController:ControllerBase
    {
        IRepository<User> _repository;

        public UserController(IRepository<User> repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task CreateAsync(UserCreationDto dto)
        {
            await _repository.InsertAsync(new User(dto.Name, dto.Email, dto.Username, dto.Password));
        }

        [HttpGet("{id}")]
        public async Task<UserDto> GetAsync(Guid id)
        {
            var user = await _repository.FirstOrDefaultAsync(x => x.Id == id);
            if (user != null)
            {
                return new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Username = user.UserName
                };
            }
            else return null;
        }

        [HttpGet]
        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _repository.ToListAsync(t=>true);
            return users!.Select(x => new UserDto
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Username = x.UserName
            }).ToList();
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            var user = await _repository.FirstOrDefaultAsync(t=>t.Id== id);
            if (user == null)
            {
                return;
            }
            await _repository.DeleteAsync(user);
        }
    }

    public class UserCreationDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
    }
}
