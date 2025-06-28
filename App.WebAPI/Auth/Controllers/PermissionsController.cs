using CleanArchMonolit.Application.Auth.DTO;
using CleanArchMonolit.Application.Auth.Interfaces.PermissionsInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace App.WebAPI.Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionsController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> CreatePermission(AddPermissionsDTO dto)
        {
            var resp = await _permissionService.CreatePermission(dto);
            return resp.Success ? Ok(resp) : BadRequest(resp);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllPermissions()
        {
            var resp = await _permissionService.GetAllPermissions();
            return resp.Success ? Ok(resp) : BadRequest(resp);
        }
    }
}
