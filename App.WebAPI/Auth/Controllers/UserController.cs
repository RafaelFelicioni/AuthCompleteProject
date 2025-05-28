using CleanArchMonolit.Application.Auth.DTO;
using CleanArchMonolit.Application.Auth.Interfaces.UserInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace App.WebAPI.Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateUserDTO dto)
        {
            var result = await _userService.CreateAsync(dto);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserDTO dto)
        {
            var result = await _userService.UpdateAsync(dto);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("GetUserInfo")]
        public async Task<IActionResult> GetUserInfo([FromQuery] int id)
        {
            var result = await _userService.GetUserInfo(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("SetEmployeeScreens")]
        public async Task<IActionResult> SetEmployeeScreens([FromBody] EmployeeScreensDTO dto)
        {
            var result = await _userService.SetEmployeeScreens(dto);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
