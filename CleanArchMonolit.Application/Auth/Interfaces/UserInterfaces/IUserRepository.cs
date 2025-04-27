using CleanArchMonolit.Domain.Entities;

namespace CleanArchMonolit.Application.Auth.Interfaces.UserInterfaces
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<User?> GetByEmailAsync(string email);
    }
}