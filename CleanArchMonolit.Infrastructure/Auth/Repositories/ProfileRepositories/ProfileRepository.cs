using CleanArchMonolit.Domain.Auth.Entities;
using CleanArchMonolit.Infrastructure.DataShared;
using CleanArchMonolit.Infrastruture.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

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

        public virtual async Task<Profiles> GetById(int id)
        {
            return await GetDbSet().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
