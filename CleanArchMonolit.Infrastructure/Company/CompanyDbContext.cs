using Microsoft.EntityFrameworkCore;

namespace CleanArchMonolit.Infrastructure.Company
{
    public class CompanyDbContext : DbContext
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options) : base(options) { }
    }
}
