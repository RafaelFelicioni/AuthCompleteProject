using CleanArchMonolit.Application.Auth.DTO;
using CleanArchMonolit.Application.Auth.Interfaces.AuthInterfaces;
using CleanArchMonolit.Application.Auth.Interfaces.UserInterfaces;
using CleanArchMonolit.Application.Auth.Validators;
using CleanArchMonolit.Domain.Auth.Entities;
using CleanArchMonolit.Infrastructure.Auth.Repositories.UserRepositories;
using CleanArchMonolit.Shared.Responses;
using CleanArchMonolit.Shared.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMonolit.Infrastructure.Auth.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly PasswordHasher<User> _hasher = new();
        private readonly IUserRepository _userRepository;

        public AuthService(IOptions<JwtSettings> jwtOptions, IUserRepository userRepository)
        {
            _jwtSettings = jwtOptions.Value;
            _userRepository = userRepository;
        }

        public async Task<Result<string>> LoginAsync(LoginDTO request)
        {
            var validator = new LoginDTOValidator();
            var validation = validator.Validate(request);

            if (!validation.IsValid)
            {
                var errors = validation.Errors.Select(e => e.ErrorMessage).ToArray();
                return Result<string>.Fail(errors);
            }

            
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
                return Result<string>.Fail("Usuário ou senha inválidos.");

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (result == PasswordVerificationResult.Failed)
                return Result<string>.Fail("Usuário ou senha inválidos.");

            var token = GenerateToken(user);
            return Result<string>.Ok(token);
        }

        public string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Profile.ProfileName),
                new Claim("ProfileId", user.Profile.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
