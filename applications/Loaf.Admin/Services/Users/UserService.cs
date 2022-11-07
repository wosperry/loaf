using AutoMapper;
using Loaf.Admin.Controllers.Users;
using Loaf.Admin.Entities;
using Loaf.Admin.Services.Users.Dtos;
using Loaf.Admin.Services.Users.Exceptions;
using Loaf.Core.Data;
using Loaf.Core.DependencyInjection;
using Loaf.Core.Encryptors;
using Loaf.EntityFrameworkCore.Repository.Extensions;
using Loaf.EntityFrameworkCore.Repository.Interfaces;

namespace Loaf.Admin.Services.Users
{

    /// <summary>
    /// 用户服务
    /// </summary>
    public class UserService : IUserService,ITransient
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly IEncryptor _encryptor;

        public UserService(IRepository<User> userRepository, IMapper mapper, IEncryptor encryptor)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _encryptor = encryptor;
        }

        /// <inheritdoc/>
        /// <exception cref="UserNotExistException">用户不存在</exception>
        /// <exception cref="UserPasswordIncorrectException">密码不正确</exception>
        public async Task<bool> CheckPasswordAsync(string account, string password)
        {
            var user = await _userRepository.FirstOrDefaultAsync(t => t.Account == account)
                ?? throw new UserNotExistException(account);

            if (user.Password != _encryptor.Encrypt(password, user.Salt))
            {
                throw new UserPasswordIncorrectException();
            }

            return true;
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        public async Task<UserDto> CreateAsync(UserCreateDto input)
        {
            var salt = Guid.NewGuid().ToString();
            var user = new User(input.Name, input.Account,_encryptor.Encrypt(input.Password,salt),salt);
            await _userRepository.InsertAsync(user);

            return _mapper.Map<UserDto>(user);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        public async Task<UserDto> GetAsync(Guid id)
        {
            var user = await _userRepository.FirstOrDefaultAsync(t => t.Id == id);
            return _mapper.Map<UserDto>(user);
        }

        /// <inheritdoc/>
        public async Task<PagedResult<UserDto>> GetPagedListAsync(UserQueryParameter input)
        {
            var query = _userRepository.GetQueryable(input);
            var res = await query.GetPagedResultAsync(input);
            return new PagedResult<UserDto>(res.Total, _mapper.Map<List<UserDto>>(res.Items)); 
        }
    }
}
