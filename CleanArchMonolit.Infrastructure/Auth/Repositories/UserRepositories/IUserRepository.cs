using CleanArchMonolit.Infrastructure.DataShared.Interface;

namespace CleanArchMonolit.Infrastructure.Auth.Repositories.UserRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
    }
}