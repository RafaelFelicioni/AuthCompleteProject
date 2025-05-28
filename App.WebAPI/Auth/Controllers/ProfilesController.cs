using CleanArchMonolit.Application.Auth.DTO;
using CleanArchMonolit.Application.Auth.Interfaces.ProfileInterfaces;
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
        public async Task<IActionResult> AddProfile([FromBody] AddProfileDTO dto)
        {
            var result = await _profileService.AddProfile(dto);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("UpdateProfile")]
        [Authorize(Policy = "UP")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDTO dto)
        {
            var result = await _profileService.UpdateProfile(dto);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
