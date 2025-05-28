using CleanArchMonolit.Application.Auth.DTO;
using CleanArchMonolit.Application.Auth.Interfaces.ProfileInterfaces;
using CleanArchMonolit.Domain.Auth.Entities;
using CleanArchMonolit.Infrastructure.Auth.Repositories.ProfileRepositories;
using CleanArchMonolit.Shared.Responses;

namespace CleanArchMonolit.Infrastructure.Auth.Services.ProfileService
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<Result<List<Profiles>>> GetAll()
        {
            var profiles = await _profileRepository.GetAll();
            if (!profiles.Any())
            {
                return Result<List<Profiles>>.Fail("Ocorreu um erro ao buscar os perfis");
            }

            return Result<List<Profiles>>.Ok(profiles);
        }

        public async Task<Result<Profiles>> GetById(int id)
        {
            var profile = await _profileRepository.GetById(id);
            if (profile == null)
            {
                return Result<Profiles>.Fail("Ocorreu um erro ao buscar o perfil");
            }

            return Result<Profiles>.Ok(profile);
        }

        public async Task<Result<bool>> UpdateProfile(UpdateProfileDTO dto)
        {
            var profile = await _profileRepository.GetById(dto.Id);
            if (profile == null)
            {
                return Result<bool>.Fail("Ocorreu um erro ao buscar o perfil");
            }

            var profileDup = await _profileRepository.FirstOrDefaultAsync(x => x.ProfileName == dto.ProfileName && x.Id != dto.Id);
            if (profileDup != null)
            {
                return Result<bool>.Fail("Já existe um perfil com este nome, por favor altere o nome e tente novamente, ou utilize o perfil: " + dto.ProfileName);
            }

            await _profileRepository.SaveChangesAsync();
            return Result<bool>.Ok(true);
        }

        public async Task<Result<bool>> AddProfile(AddProfileDTO dto)
        {
            var profile = await _profileRepository.FirstOrDefaultAsync(x => x.ProfileName == dto.ProfileName);
            if (profile == null)
            {
                return Result<bool>.Fail("Ocorreu um erro ao buscar o perfil");
            }

            profile.ProfileName = dto.ProfileName;

            await _profileRepository.SaveChangesAsync();
            return Result<bool>.Ok(true);
        }
    }
}
