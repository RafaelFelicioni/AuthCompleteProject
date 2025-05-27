using CleanArchMonolit.Infrastructure.Auth.Services.ProfileService;
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
        public async Task<IActionResult> GetAll()
        {
            var result = await _profileService.GetAll();
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
