using CleanArchMonolit.Application.Auth.DTO;
using CleanArchMonolit.Application.Auth.Interfaces.AuthInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Auth.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var result = await _authService.LoginAsync(dto);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
