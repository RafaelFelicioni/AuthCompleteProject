using CleanArchMonolit.Application.Auth.Interfaces.PermissionsInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace App.WebAPI.Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionInterface _permissionService;

        public PermissionsController(IPermissionInterface permissionService)
        {
            _permissionService = permissionService;
        }
    }
}
