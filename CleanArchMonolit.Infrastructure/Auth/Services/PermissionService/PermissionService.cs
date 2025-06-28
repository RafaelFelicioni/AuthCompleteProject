using CleanArchMonolit.Application.Auth.DTO;
using CleanArchMonolit.Application.Auth.Interfaces.PermissionsInterfaces;
using CleanArchMonolit.Domain.Auth.Entities;
using CleanArchMonolit.Infrastructure.Auth.Repositories.PermissionRepositories;
using CleanArchMonolit.Infrastructure.DataShared.HttpContextService;
using CleanArchMonolit.Shared.Responses;

namespace CleanArchMonolit.Infrastructure.Auth.Services.PermissionService
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _repo;
        private readonly IHttpContextService _httpContext;

        public PermissionService(IPermissionRepository repo, IHttpContextService httpContextService)
        {
            _repo = repo;
            _httpContext = httpContextService;
        }

        public async Task<Result<bool>> CreatePermission(AddPermissionsDTO dto)
        {
            var isAdmin = _httpContext.IsAdmin;
            if (!isAdmin)
            {
                return Result<bool>.Fail("Ocorreu um erro ao criar a pemissão, apenas usuários administradores podem criar permissões");
            }

            var hasPermissionWithSameName = await _repo.HasPermissionWithSameName(dto.PermissionName);
            if (hasPermissionWithSameName)
                return Result<bool>.Fail($"Já existe uma permissão com o nome {dto.PermissionName}");
            var hasPermissionWIthSameCode = await _repo.HasPermissionWithSameCode(dto.PermissionCode);
            if (hasPermissionWithSameName)
                return Result<bool>.Fail($"Já existe uma permissão com o codigo {dto.PermissionCode}");

            var newPermission = new SystemPermission()
            {
                PermissionCode = dto.PermissionCode,
                PermissionName = dto.PermissionName,
                AdminOnly = dto.AdminOnly,
                UserPermissions = new List<UserSystemPermissions>()
            };

            await _repo.AddAsync(newPermission);
            await _repo.SaveChangesAsync();
            return Result<bool>.Ok(true);
        }

        public async Task<Result<List<SystemPermission>>> GetAllPermissions()
        {
            var isAdmin = _httpContext.IsAdmin;
            var allPermissions = await _repo.GetAllPermissions(isAdmin);
            return Result<List<SystemPermission>>.Ok(allPermissions);
        }
    }
}
