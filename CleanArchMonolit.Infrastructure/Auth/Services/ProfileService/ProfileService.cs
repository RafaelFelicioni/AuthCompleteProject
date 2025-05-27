using CleanArchMonolit.Domain.Auth.Entities;
using CleanArchMonolit.Infrastructure.Auth.Repositories.ProfileRepositories;
using CleanArchMonolit.Shared.Responses;

namespace CleanArchMonolit.Infrastructure.Auth.Services.ProfileService
{
    public class ProfileService
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
    }
}
