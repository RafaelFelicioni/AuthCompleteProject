using CleanArchMonolit.Domain.Auth.Entities;
using CleanArchMonolit.Infrastructure.DataShared.Interface;

namespace CleanArchMonolit.Infrastructure.Auth.Repositories.PermissionRepositories
{
    public interface IPermissionRepository : IRepository<SystemPermission>
    {
        Task<bool> HasPermissionWithSameCode(string code);
        Task<bool> HasPermissionWithSameName(string permissionName);
        Task<List<SystemPermission>> GetAllPermissions(bool isAdmin);
    }
}
