using CleanArchMonolit.Infrastructure.Company.Data;
using CleanArchMonolit.Infrastructure.DataShared;
using Microsoft.AspNetCore.Http;
using CompanyEntity = CleanArchMonolit.Domain.Company.Entities.Company;

namespace CleanArchMonolit.Infrastructure.Company.Repositories.CompanyRepository
{

    public class CompanyRepository : BaseRepository<CompanyEntity, CompanyDbContext>, ICompanyRepository
    {
        private readonly CompanyDbContext _context;
        private readonly HttpContext _httpContext;
        public CompanyRepository(CompanyDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
            _context = context;
            _httpContext = httpContextAccessor?.HttpContext;
        }
    }
}
