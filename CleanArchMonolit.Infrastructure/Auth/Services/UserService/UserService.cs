using CleanArchMonolit.Application.Auth.DTO;
using CleanArchMonolit.Application.Auth.Interfaces.UserInterfaces;
using CleanArchMonolit.Application.Auth.Validators;
using CleanArchMonolit.Domain.Auth.Entities;
using CleanArchMonolit.Infrastructure.Auth.Repositories.ProfileRepositories;
using CleanArchMonolit.Infrastructure.Auth.Repositories.UserRepositories;
using CleanArchMonolit.Infrastructure.DataShared.HttpContextService;
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
        private readonly IHttpContextService _httpContext;
        private readonly PasswordHasher<User> _hasher = new();
        public UserService(IUserRepository userRepository, IHttpContextService httpContext, IProfileRepository profileRepository)
        {
            _userRepository = userRepository;
            _profileRepository = profileRepository;
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

            user = new User()
            {
                Mail = dto.Email,
                ProfileId = dto.ProfileId,
                Username = dto.Username,
                CompanyId = dto.CompanyId,
                UserPermissions = new List<UserSystemPermissions>()
            };

            if (dto.PermissionList.Any())
            {
                foreach (var permission in dto.PermissionList)
                {
                    user.UserPermissions.Add(new UserSystemPermissions()
                    {
                        UserId = user.Id,
                        SystemPermissionId = permission
                    });
                }
            }

            user.PasswordHash = _hasher.HashPassword(user, dto.Password);
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
            return Result<bool>.Ok(true);
        }

        public async Task<Result<bool>> UpdateAsync(UpdateUserDTO dto)
        {
            var validator = new UpdateUserDTOValidator();
            var validation = validator.Validate(dto);
            if (!validation.IsValid)
            {
                var errors = validation.Errors.Select(e => e.ErrorMessage).ToArray();
                return Result<bool>.Fail(errors);
            }

            var userValidation = await _userRepository.FirstOrDefaultAsync(x => x.Id != dto.Id && (x.Mail == dto.Email));
            if (userValidation != null)
                return Result<bool>.Fail("Este email já está cadastrado para outro usuário, por favor altere o email e tente novamente");

            var user = await _userRepository.FindAsync(dto.Id);
            if (user == null)
                return Result<bool>.Fail("Não foi possivel encontrar o usuário informado, por favor entre em contato com o suporte.");

            user.Mail = dto.Email;
            user.ProfileId = dto.ProfileId;
            user.Username = dto.Username;
            if (user.UserPermissions == null)
                user.UserPermissions = new List<UserSystemPermissions>();

            var userPermissions = user.UserPermissions.Select(x => x.SystemPermissionId).ToList();
            dto.PermissionList = dto.PermissionList.Where(x => !userPermissions.Contains(x)).ToList();
            foreach (var permission in dto.PermissionList)
            {
                user.UserPermissions.Add(new UserSystemPermissions()
                {
                    UserId = user.Id,
                    SystemPermissionId = permission
                });
            }

            await _userRepository.SaveChangesAsync();
            return Result<bool>.Ok(true);
        }

        public async Task<Result<bool>> ChangePasswordUser(string oldPassword, string newPassword)
        {
            var user = await _userRepository.GetById(_httpContext.UserId);
            if (user == null)
                Result<bool>.Fail("Não foi possivel encontrar o usuário informado");

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, oldPassword);
            if (result == PasswordVerificationResult.Failed)
                return Result<bool>.Fail("Senha antiga informada não corresponde a senha na base de dados, por favor tente novamente.");

            user.PasswordHash = _hasher.HashPassword(user, newPassword);
            return Result<bool>.Ok(true);
        }

        public async Task<Result<User>> GetUserInfo(int id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
            {
                Result<User>.Fail("Não foi possivel encontrar o usuário informado");
            }

            return Result<User>.Ok(user);
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
                CompanyName = x.CompanyName,
                ProfileName = x.ProfileName,
                UserName = x.UserName,
            }).Skip(dto.Skip).Take(dto.Take).ToListAsync();

            return Result<GridResponseDTO<ReturnUsersGridDTO>>.Ok(response);
        }
    }
}
