using CleanArchMonolit.Domain.Entities;
using CleanArchMonolit.Shared.Infrastructure.Data.Interface;

namespace CleanArchMonolit.Infrastructure.Auth.Repositories.UserRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
    }
}