using CleanArchMonolit.Application.Auth.DTO;
using CleanArchMonolit.Shared.DTO;
using CleanArchMonolit.Shared.DTO.Grid;
using CleanArchMonolit.Shared.Responses;

namespace CleanArchMonolit.Application.Auth.Interfaces.UserInterfaces
{
    public interface IUserService
    {
        Task<Result<bool>> CreateAsync(CreateUserDTO user);
        Task<Result<bool>> UpdateAsync(UpdateUserDTO user);
        Task<Result<UserDTO>> GetUserInfo(int id);
        Task<Result<bool>> ChangePasswordUser(string oldPassword, string newPassword);
        Task<Result<GridResponseDTO<ReturnUsersGridDTO>>> GetUsersGrid(GetUsersGrid dto);
        Task<Result<List<SelectPatternDTO>>> SelectUsers(string term);
    }
}
