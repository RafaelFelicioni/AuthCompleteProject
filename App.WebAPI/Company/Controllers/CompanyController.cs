using CleanArchMonolit.Application.Company.Interfaces.CompanyInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.WebAPI.Company.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        // Criar empresa
        [HttpPost("Create")]
        [Authorize(Policy = "")]
        public async Task<IActionResult> CreateCompany()
        {
            var resp = _companyService
        }

        // Atualizar empresa
        [HttpPost("Update")]
        [Authorize(Policy = "")]

        // Grid das empresas (somente admin)
        [HttpPost("Grid")]
        [Authorize(Policy = "")]

        [HttpGet("GetById")]
        [Authorize(Policy = "")]

        // GetById
        [HttpGet("GetList")]
        [Authorize(Policy = "")]

    }
}
