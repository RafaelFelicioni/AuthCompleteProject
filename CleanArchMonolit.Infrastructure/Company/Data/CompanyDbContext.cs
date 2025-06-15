using Microsoft.EntityFrameworkCore;
using CompanyEntity = CleanArchMonolit.Domain.Company.Entities.Company;

namespace CleanArchMonolit.Infrastructure.Company.Data
{
    public class CompanyDbContext : DbContext
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options) : base(options) { }

        public DbSet<CompanyEntity> Companies { get; set; }
    }
}
