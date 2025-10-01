using NavQurt.Server.Application.Dto_s;

namespace NavQurt.Server.Application.Services;

public interface IRoleService
{
    Task<ICollection<UserGetDto>> GetAllUsersByRoleAsync(string role);
    Task<List<RoleGetDto>> GetAllRolesAsync();
    Task<long> GetRoleIdAsync(string role);
}