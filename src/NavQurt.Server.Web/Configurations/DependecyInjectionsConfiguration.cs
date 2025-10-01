using FluentValidation;
using NavQurt.Server.Application.Dto_s;
using NavQurt.Server.Application.Helpers;
using NavQurt.Server.Application.Interfaces;
using NavQurt.Server.Application.Services;
using NavQurt.Server.Application.Validators;
using NavQurt.Server.Infrastructure.Persistence.Repositories;

namespace NavQurt.Server.Web.Configurations;

public static class DependecyInjectionsConfiguration
{
    public static void ConfigureDependecies(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IRoleRepository, UserRoleRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IValidator<UserCreateDto>, UserCreateDtoValidator>();
        services.AddScoped<IValidator<UserLoginDto>, UserLoginDtoValidator>();
    }
}
