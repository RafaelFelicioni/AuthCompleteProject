#nullable disable
using CleanArchMonolit.Application.Auth.DTO;
using CleanArchMonolit.Domain.Auth.Entities;
using CleanArchMonolit.Infrastructure.DataShared;
using CleanArchMonolit.Infrastruture.Data;
using CleanArchMonolit.Shared.DTO;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMonolit.Infrastructure.Auth.Repositories.UserRepositories
{
    public class UserRepository : BaseRepository<User, AuthDbContext>, IUserRepository
    {
        private readonly AuthDbContext _context;

        public UserRepository(AuthDbContext context) : base(context)
        {
            _context = context;
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
            return await GetDbSet().Include(x => x.UserPermissions).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> CheckIfTaxIdExists(string taxId)
        {
            return await GetDbSet().AnyAsync(x => x.TaxId == taxId);
        }

        public IQueryable<ReturnUsersGridDTO> GetUserGrid(GetUsersGrid dto)
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

        public async Task<List<SelectPatternDTO>> GetSelectUser(string term, int companyId, bool isAdmin)
        {
            return await GetDbSet().Where(x => (!isAdmin && x.CompanyId == companyId && (EF.Functions.Like(x.TaxId, $"%{term}%")
                || EF.Functions.Like(x.Username, $"%{term}%"))) ||
                (isAdmin && (EF.Functions.Like(x.TaxId, $"%{term}%") || EF.Functions.Like(x.Username, $"%{term}%")))
                    ).Select(x => new SelectPatternDTO()
                    {
                        Value = x.Id,
                        Info = $"{x.Username} (${x.TaxId})"
                    }).ToListAsync();
        }
    }
}
