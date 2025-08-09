using CleanArchMonolit.Application.Auth.DTO;
using CleanArchMonolit.Domain.Auth.Entities;
using CleanArchMonolit.Infrastructure.DataShared.Interface;
using CleanArchMonolit.Shared.DTO;

namespace CleanArchMonolit.Infrastructure.Auth.Repositories.UserRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
        Task<User> GetById(int id);
        IQueryable<ReturnUsersGridDTO> GetUserGrid(GetUsersGrid dto);
        Task<List<SelectPatternDTO>> GetSelectUser(string term, int companyId, bool isAdmin);
        Task<bool> CheckIfTaxIdExists(string taxId);
    }
}