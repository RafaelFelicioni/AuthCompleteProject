using App.WebAPI.Utils;
using CleanArchMonolit.Infrastructure.Company.Data;
using CleanArchMonolit.Infrastruture.Data;
using CleanArchMonolit.Shared.Extensions;
using CleanArchMonolit.Shared.Middlewares;
using CleanArchMonolit.Shared.Responses;
using CleanArchMonolit.Shared.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<CompanyDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthorization(options =>
{
    var permissions = new[] { "" }; // TODO RAFAEL: colocar todas as permiss�es aqui

    foreach (var permission in permissions)
    {
        options.AddPolicy(permission, policy =>
            policy.Requirements.Add(new PermissionRequirement(permission)));
    }
});
builder.Services.Configure<JwtSettings>(
builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "AuthService",
            ValidAudience = "AuthClient",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.Secret))
        };
        var statusCodeExpired = 440;
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                if (context.Exception is SecurityTokenExpiredException)
                {
                    context.Response.Headers.Add("Token-Expired", "true");
                }
                return Task.CompletedTask;
            },

            OnChallenge = context =>
            {
                context.HandleResponse();

                var expired = context.Response.Headers.ContainsKey("Token-Expired");
                var code = expired ? statusCodeExpired : StatusCodes.Status401Unauthorized;
                var message = expired
                    ? "Sess�o expirada, por favor fa�a login novamente."
                    : "Fa�a login para continuar.";

                context.Response.StatusCode = code;
                context.Response.ContentType = "application/json";
                var resp = Result<object>.Fail(message); ;
                var payload = JsonSerializer.Serialize(resp);
                return context.Response.WriteAsync(payload);
            }
        };
    });
builder.Services.AddHttpContextAccessor();
builder.AddClients();
builder.AddServices();
builder.AddRepositories();
var app = builder.Build();
app.UseRouting();
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.Run();
