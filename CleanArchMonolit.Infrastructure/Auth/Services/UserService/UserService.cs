using Azure.Core;
using CleanArchMonolit.Application.Auth.DTO;
using CleanArchMonolit.Application.Auth.Interfaces.UserInterfaces;
using CleanArchMonolit.Application.Auth.Validators;
using CleanArchMonolit.Domain.Entities;
using CleanArchMonolit.Infrastructure.Auth.Repositories.UserRepositories;
using CleanArchMonolit.Shared.Responses;
using Microsoft.AspNetCore.Identity;

namespace CleanArchMonolit.Infrastructure.Auth.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<User> _hasher = new();
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<bool>> CreateAsync(CreateUserDTO dto)
        {
            var validator = new CreateUserDTOValidator();
            var validation = validator.Validate(dto);
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
            };

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

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, dto.OldPassword);
            if (result == PasswordVerificationResult.Failed)
                return Result<bool>.Fail("Senha antiga informada não corresponde a senha na base de dados, por favor tente novamente.");

            user.PasswordHash = _hasher.HashPassword(user, dto.NewPassword);
            user.Mail = dto.Email;
            user.ProfileId = dto.ProfileId;
            user.Username = dto.Username;

            await _userRepository.SaveChangesAsync();
            return Result<bool>.Ok(true);
        }
    }
}
