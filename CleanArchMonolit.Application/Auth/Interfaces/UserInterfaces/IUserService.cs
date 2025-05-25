using CleanArchMonolit.Application.Auth.DTO;
using CleanArchMonolit.Domain.Entities;
using CleanArchMonolit.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMonolit.Application.Auth.Interfaces.UserInterfaces
{
    public interface IUserService
    {
        Task<Result<bool>> CreateAsync(CreateUserDTO user);
        Task<Result<bool>> UpdateAsync(UpdateUserDTO user);
    }
}
