using CleanArchMonolit.Application.Auth.DTO;
using CleanArchMonolit.Domain.Auth.Entities;
using CleanArchMonolit.Shared.Responses;

namespace CleanArchMonolit.Application.Auth.Interfaces.ProfileInterfaces
{
    public interface IProfileService
    {
        Task<Result<List<Profiles>>> GetAll();
        Task<Result<Profiles>> GetById(int id);
        Task<Result<bool>> AddProfile(AddProfileDTO dto);
        Task<Result<bool>> UpdateProfile(UpdateProfileDTO dto);
    }
}
