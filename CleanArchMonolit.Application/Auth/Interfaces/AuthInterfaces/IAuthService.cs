using CleanArchMonolit.Application.Auth.DTO;
using CleanArchMonolit.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMonolit.Application.Auth.Interfaces.AuthInterfaces
{
    public interface IAuthService
    {
        Task<Result<string>> LoginAsync(LoginDTO request);
    }
}
