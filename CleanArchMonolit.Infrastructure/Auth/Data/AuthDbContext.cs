using CleanArchMonolit.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMonolit.Infrastruture.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

        public DbSet<Profiles> Profiles { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profiles>().HasData(
                new Profiles { Id = 1, ProfileName = "Admin" },
                new Profiles { Id = 2, ProfileName = "CompanyOwner" },
                new Profiles { Id = 3, ProfileName = "User" },
                new Profiles { Id = 4, ProfileName = "Employee" }
            );
        }
    }
}
