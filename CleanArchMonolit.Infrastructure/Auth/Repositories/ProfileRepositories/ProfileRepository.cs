using CleanArchMonolit.Domain.Auth.Entities;
using CleanArchMonolit.Infrastructure.DataShared;
using CleanArchMonolit.Infrastruture.Data;
using Microsoft.AspNetCore.Http;

namespace CleanArchMonolit.Infrastructure.Auth.Repositories.ProfileRepositories
{
    public class ProfileRepository : BaseRepository<Profiles, AuthDbContext>, IProfileRepository
    {
        private readonly AuthDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileRepository(AuthDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
    }
}
