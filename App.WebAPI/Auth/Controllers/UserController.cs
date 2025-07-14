using CleanArchMonolit.Application.Auth.DTO;
using CleanArchMonolit.Application.Auth.Interfaces.UserInterfaces;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Policy = "")]
        public async Task<IActionResult> Create([FromBody] CreateUserDTO dto)
        {
            var result = await _userService.CreateAsync(dto);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("Update")]
        [Authorize(Policy = "test")]
        public async Task<IActionResult> Update([FromBody] UpdateUserDTO dto)
        {
            var result = await _userService.UpdateAsync(dto);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("GetUserInfo")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> GetUserInfo([FromQuery] int id)
        {
            var result = await _userService.GetUserInfo(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("ChangePassword")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> ChangePasswordUser(string oldPassword, string newPassword)
        {
            var result = await _userService.ChangePasswordUser(oldPassword, newPassword);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("GetUsersGrid")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> GetUsersGrid(GetUsersGrid dto)
        {
            var result = await _userService.GetUsersGrid(dto);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
