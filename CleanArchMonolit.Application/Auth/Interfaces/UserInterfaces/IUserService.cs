using CleanArchMonolit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMonolit.Application.Auth.Interfaces.UserInterfaces
{
    public interface IUserService
    {
        Task<User> CreateAsync(User user);
    }
}
