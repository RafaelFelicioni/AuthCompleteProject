using CleanArchMonolit.Domain.Auth.Entities;
using CleanArchMonolit.Infrastructure.DataShared;
using CleanArchMonolit.Infrastruture.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMonolit.Infrastructure.Auth.Repositories.UserRepositories
{
    public class UserRepository : BaseRepository<User, AuthDbContext>, IUserRepository
    {
        private readonly AuthDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepository(AuthDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await GetDbSet()
                .Include(u => u.Profile) // se precisar do perfil no JWT
                .FirstOrDefaultAsync(u => u.Mail == email);
        }
    }
}
