using CleanArchMonolit.Application.Auth.DTO;
using CleanArchMonolit.Application.Auth.Interfaces.UserInterfaces;
using CleanArchMonolit.Application.Auth.Validators;
using CleanArchMonolit.Application.Company.Interfaces.CompanyInterfaces;
using CleanArchMonolit.Domain.Auth.Entities;
using CleanArchMonolit.Infrastructure.Auth.Repositories.ProfileRepositories;
using CleanArchMonolit.Infrastructure.Auth.Repositories.UserRepositories;
using CleanArchMonolit.Infrastructure.DataShared.HttpContextService;
using CleanArchMonolit.Shared.DTO;
using CleanArchMonolit.Shared.DTO.Grid;
using CleanArchMonolit.Shared.Extensions;
using CleanArchMonolit.Shared.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArchMonolit.Infrastructure.Auth.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly ICompanyService _companyService;
        private readonly IHttpContextService _httpContext;
        private readonly PasswordHasher<User> _hasher = new();
        public UserService(IUserRepository userRepository, IHttpContextService httpContext, IProfileRepository profileRepository, ICompanyService companyService)
        {
            _userRepository = userRepository;
            _profileRepository = profileRepository;
            _companyService = companyService;
            _httpContext = httpContext;
        }

        public async Task<Result<bool>> CreateAsync(CreateUserDTO dto)
        {
            var validator = new CreateUserDTOValidator();
            var validation = validator.Validate(dto);
            var isAdmin = _httpContext.IsAdmin;
            var userCompanyId = _httpContext.CompanyId;

            if (!isAdmin && userCompanyId != dto.CompanyId)
            {
                return Result<bool>.Fail("Não foi possível criar o usuário, por favor entre em contato com os canais de comunicação");
            }

            if (!validation.IsValid)
            {
                var errors = validation.Errors.Select(e => e.ErrorMessage).ToArray();
                return Result<bool>.Fail(errors);
            }

            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user != null)
            {
                return Result<bool>.Fail("Usuário já existe na base de dados com este email");
            }

            var checkTaxId = await _userRepository.CheckIfTaxIdExists(dto.TaxId);
            if (checkTaxId)
            {
                return Result<bool>.Fail("Já existe um usuário cadastrado com esse CPF, por favor corrija as informações e tente novamente.");
            }

            var profile = await _profileRepository.GetById(dto.ProfileId);
            if (profile is null)
                return Result<bool>.Fail("Perfil inválido.");

            

            var newUser = new User(
                id: 0, // EF
                active: true,
                username: dto.Username,
                mail: dto.Email,
                passwordHash: string.Empty,
                companyId: dto.CompanyId,
                profileId: dto.ProfileId,
                taxId: dto.TaxId,
                profile: profile
            );
            var hashedPassword = _hasher.HashPassword(user, dto.Password);
            newUser.SetPasswordHash(hashedPassword);
            // Let the aggregate manage its children
            if (dto.PermissionList?.Any() == true)
                newUser.AddPermissions(dto.PermissionList);

            await _userRepository.AddAsync(newUser);
            await _userRepository.SaveChangesAsync();
            return Result<bool>.Ok(true);
        }

        public async Task<Result<bool>> UpdateAsync(UpdateUserDTO dto)
        {
            var validator = new UpdateUserDTOValidator();
            var validation = validator.Validate(dto);
            if (!validation.IsValid)
                return Result<bool>.Fail(validation.Errors.Select(e => e.ErrorMessage).ToArray());

            var emailTaken = await _userRepository
                .FirstOrDefaultAsync(x => x.Id != dto.Id && x.Mail == dto.Email);
            if (emailTaken != null)
                return Result<bool>.Fail("Este email já está cadastrado para outro usuário, por favor altere o email e tente novamente");

            var user = await _userRepository
                .FindAsync(dto.Id);

            if (user == null)
                return Result<bool>.Fail("Não foi possivel encontrar o usuário informado, por favor entre em contato com o suporte.");

            user.ChangeEmail(dto.Email);
            user.Rename(dto.Username);
            user.MoveToCompany(dto.CompanyId);
            user.ChangeProfile(dto.ProfileId);

            user.ReplacePermissions(dto.PermissionList ?? Enumerable.Empty<int>());

            await _userRepository.SaveChangesAsync();
            return Result<bool>.Ok(true);
        }

        public async Task<Result<bool>> ChangePasswordUser(string oldPassword, string newPassword)
        {
            var user = await _userRepository.GetById(_httpContext.UserId);
            if (user == null)
                return Result<bool>.Fail("Não foi possivel encontrar o usuário informado");

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, oldPassword);
            if (result == PasswordVerificationResult.Failed)
                return Result<bool>.Fail("Senha antiga informada não corresponde a senha na base de dados, por favor tente novamente.");

            user.SetPasswordHash(_hasher.HashPassword(user, newPassword));
            return Result<bool>.Ok(true);
        }

        public async Task<Result<UserDTO>> GetUserInfo(int id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
            {
                return Result<UserDTO>.Fail("Não foi possivel encontrar o usuário informado");
            }

            UserDTO userDTO = user;
            //var company = await _companyService.GetCompanyInfo(user.CompanyId);
            //if (company != null && company.Success)
            //{
            //    userDTO.CompanyName = company.Data.CompanyName;
            //    userDTO.CompanyTaxId = company.Data.TaxId;
            //}
            return Result<UserDTO>.Ok(user);
        }

        public async Task<Result<GridResponseDTO<ReturnUsersGridDTO>>> GetUsersGrid(GetUsersGrid dto)
        {
            var response = new GridResponseDTO<ReturnUsersGridDTO>();
            var userIsAdmin = _httpContext.IsAdmin;
            if (!userIsAdmin)
                dto.CompanyId = _httpContext.CompanyId;
            else
                dto.CompanyId = null;

            var sortDir = false;
            if (string.IsNullOrWhiteSpace(dto.SortDirection) || dto.SortDirection.ToLower() == "asc")
            {
                sortDir = true;
            }
            var usersQueryable = _userRepository.GetUserGrid(dto);
            usersQueryable.LinqOrderBy(dto.SortBy, sortDir);

            response.TotalItems = usersQueryable.Count();
            response.Page = dto.Page;
            response.PageSize = dto.PageSize;
            response.Items = await usersQueryable.Select(x => new ReturnUsersGridDTO()
            {
                CompanyId = x.CompanyId,
                ProfileName = x.ProfileName,
                UserName = x.UserName,
            }).Skip(dto.Skip).Take(dto.Take).ToListAsync();
            if (response.Items.Count > 0)
            {
                var companyIds = response.Items.Select(x => x.CompanyId).ToList();
                //var companies = await _companyService.GetCompanyList(companyIds);
                foreach (var item in response.Items)
                {
                    //var company = companies.Data.FirstOrDefault(x => x.Id == item.CompanyId);
                    //if (company != null)
                    //{
                    //    item.CompanyName = company.CompanyName;
                    //}
                }
            }
            return Result<GridResponseDTO<ReturnUsersGridDTO>>.Ok(response);
        }

        public async Task<Result<List<SelectPatternDTO>>> SelectUsers(string term)
        {
            var isAdmin = _httpContext.IsAdmin;
            var companyid = _httpContext.CompanyId;

            var usersSelect = await _userRepository.GetSelectUser(term, companyid, isAdmin);
            if (usersSelect != null)
            {
                return Result<List<SelectPatternDTO>>.Ok(usersSelect);
            }

            return Result<List<SelectPatternDTO>>.Fail("Ocorreu um erro ao buscar os usuários");
        }
    }
}
