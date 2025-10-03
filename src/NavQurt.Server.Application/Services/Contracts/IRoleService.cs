using NavQurt.Server.Application.Dto.Auth;

namespace NavQurt.Server.Application.Services.Contracts;

public interface IRoleService
{
    Task<ICollection<UserGetDto>> GetAllUsersByRoleAsync(string role);
    Task<List<RoleGetDto>> GetAllRolesAsync();
    Task<long> GetRoleIdAsync(string role);
}