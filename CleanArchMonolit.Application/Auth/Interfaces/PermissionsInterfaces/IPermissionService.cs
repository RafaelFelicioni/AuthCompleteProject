using CleanArchMonolit.Application.Auth.DTO;
using CleanArchMonolit.Domain.Auth.Entities;
using CleanArchMonolit.Shared.Responses;

namespace CleanArchMonolit.Application.Auth.Interfaces.PermissionsInterfaces
{
    public interface IPermissionService
    {
        Task<Result<bool>> CreatePermission(AddPermissionsDTO dto);
        Task<Result<List<SystemPermission>>> GetAllPermissions();
    }
}
