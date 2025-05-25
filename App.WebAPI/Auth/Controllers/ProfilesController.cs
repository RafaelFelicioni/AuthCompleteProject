using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace App.WebAPI.Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        public ProfilesController()
        {
         
            
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetProfiles()
        {
            
        }
    }
}
