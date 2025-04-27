using CleanArchMonolit.Application.Auth.Interfaces.UserInterfaces;
using CleanArchMonolit.Application.Auth.Interfaces.UserInterfaces.Repositories;
using CleanArchMonolit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMonolit.Infrastructure.Auth.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> CreateAsync(User user)
        {
            await _userRepository.AddAsync(user);
            return user;
        }
    }
}
