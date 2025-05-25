using CleanArchMonolit.Domain.Entities;
using CleanArchMonolit.Infrastructure.Auth.Data;
using CleanArchMonolit.Infrastructure.Auth.Repositories.UserRepositories;
using CleanArchMonolit.Infrastruture.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
