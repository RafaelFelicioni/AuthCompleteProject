using CleanArchMonolit.Application.Auth.Interfaces.AuthInterfaces;
using CleanArchMonolit.Application.Auth.Interfaces.PermissionsInterfaces;
using CleanArchMonolit.Application.Auth.Interfaces.ProfileInterfaces;
using CleanArchMonolit.Application.Auth.Interfaces.UserInterfaces;
using CleanArchMonolit.Infrastructure.Auth.Repositories.PermissionRepositories;
using CleanArchMonolit.Infrastructure.Auth.Repositories.ProfileRepositories;
using CleanArchMonolit.Infrastructure.Auth.Repositories.UserRepositories;
using CleanArchMonolit.Infrastructure.Auth.Services.AuthService;
using CleanArchMonolit.Infrastructure.Auth.Services.PermissionService;
using CleanArchMonolit.Infrastructure.Auth.Services.ProfileService;
using CleanArchMonolit.Infrastructure.Auth.Services.UserService;
using CleanArchMonolit.Infrastructure.DataShared.HttpContextService;
using CleanArchMonolit.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace App.WebAPI.Utils
{
    public static class ServiceRegistration
    {
        public static void AddClients(this WebApplicationBuilder builder)
        {
            builder.Services.AddHttpContextAccessor();
        }

        public static void AddServices(this WebApplicationBuilder builder)
        {
            #region AuthServices
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IProfileService, ProfileService>();
            builder.Services.AddScoped<IPermissionService, PermissionService>();
            #endregion

            builder.Services.AddScoped<IHttpContextService, HttpContextService>();
            builder.Services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
            builder.Services.ApplyCommonSettings(builder.Configuration);
        }

        public static void AddRepositories(this WebApplicationBuilder builder)
        {
            #region AuthRepos
            builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
            #endregion
        }
    }
}
