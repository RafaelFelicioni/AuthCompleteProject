using CleanArchMonolit.Application.Auth.DTO;
using CleanArchMonolit.Domain.Auth.Entities;
using CleanArchMonolit.Infrastructure.DataShared.Interface;

namespace CleanArchMonolit.Infrastructure.Auth.Repositories.UserRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
        Task<User> GetById(int id);
        Task<IQueryable<ReturnUsersGridDTO>> GetUserGrid(GetUsersGrid dto);
    }
}