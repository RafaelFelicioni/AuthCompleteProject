#nullable disable
using CleanArchMonolit.Application.Auth.DTO;
using CleanArchMonolit.Domain.Auth.Entities;
using CleanArchMonolit.Infrastructure.DataShared;
using CleanArchMonolit.Infrastruture.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMonolit.Infrastructure.Auth.Repositories.UserRepositories
{
    public class UserRepository : BaseRepository<User, AuthDbContext>, IUserRepository
    {
        private readonly AuthDbContext _context;
        private readonly HttpContext _httpContext;

        public UserRepository(AuthDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
            _context = context;
            _httpContext = httpContextAccessor?.HttpContext;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await GetDbSet()
                .Include(u => u.Profile)// se precisar do perfil no JWT
                .Include(x => x.UserPermissions)
                .FirstOrDefaultAsync(u => u.Mail == email);
        }

        public async Task<User> GetById(int id)
        {
            return await GetDbSet().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IQueryable<ReturnUsersGridDTO>> GetUserGrid(GetUsersGrid dto)
        {
            return GetDbSet().Where(x => ((!dto.CompanyId.HasValue) || (dto.CompanyId.HasValue && dto.CompanyId.Value > 0 && x.CompanyId == dto.CompanyId.Value)) &&
                (!dto.UserId.HasValue || (dto.UserId.HasValue && dto.UserId.Value < 1) || (dto.UserId.HasValue && dto.UserId.Value == x.Id)) &&
                (!dto.ProfileId.HasValue || (dto.ProfileId.HasValue && dto.ProfileId.Value < 1) || (dto.ProfileId.HasValue && dto.ProfileId.Value == x.Id))
             ).Select(x => new ReturnUsersGridDTO()
             {
                 UserName = x.Username,
                 ProfileName = x.Profile.ProfileName,
                 CompanyId = x.CompanyId
             });
        }
    }
}
