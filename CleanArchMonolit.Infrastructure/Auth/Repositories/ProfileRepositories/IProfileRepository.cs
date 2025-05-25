using CleanArchMonolit.Domain.Entities;
using CleanArchMonolit.Shared.Infrastructure.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMonolit.Infrastructure.Auth.Repositories.ProfileRepositories
{
    public interface IProfileRepository : IRepository<Profiles>
    {
    }
}
