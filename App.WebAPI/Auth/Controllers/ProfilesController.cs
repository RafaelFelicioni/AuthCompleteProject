using CleanArchMonolit.Infrastructure.Auth.Services.ProfileService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.WebAPI.Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfilesController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet("GetAll")]
        [Authorize(Policy = "GP")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _profileService.GetAll();
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetById")]
        [Authorize(Policy = "PostLogin")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            var result = await _profileService.GetById(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("AddProfile")]
        [Authorize(Policy = "AP")]
        public async Task<IActionResult> AddProfile([FromQuery] int id)
        {
            var result = await _profileService.GetById(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("UpdateProfile")]
        [Authorize(Policy = "UP")]
        public async Task<IActionResult> UpdateProfile([FromQuery] int id)
        {
            var result = await _profileService.GetById(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
