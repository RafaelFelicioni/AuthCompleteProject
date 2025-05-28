using CleanArchMonolit.Domain.Auth.Entities;
using CleanArchMonolit.Shared.Responses;

namespace CleanArchMonolit.Infrastructure.Auth.Services.ProfileService
{
    public interface IProfileService
    {
        Task<Result<List<Profiles>>> GetAll();
        Task<Result<Profiles>> GetById(int id);
    }
}
