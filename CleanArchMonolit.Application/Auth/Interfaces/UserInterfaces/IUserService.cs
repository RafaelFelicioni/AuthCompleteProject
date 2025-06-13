using CleanArchMonolit.Application.Auth.DTO;
using CleanArchMonolit.Domain.Auth.Entities;
using CleanArchMonolit.Shared.Responses;

namespace CleanArchMonolit.Application.Auth.Interfaces.UserInterfaces
{
    public interface IUserService
    {
        Task<Result<bool>> CreateAsync(CreateUserDTO user);
        Task<Result<bool>> UpdateAsync(UpdateUserDTO user);
        Task<Result<User>> GetUserInfo(int id);
        Task<Result<bool>> ChangePasswordUser(string oldPassword, string newPassword);
    }
}
