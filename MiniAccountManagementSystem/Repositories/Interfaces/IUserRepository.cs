using MiniAccountManagementSystem.Models;
using static MiniAccountManagementSystem.Repositories.UserRepository;

namespace MiniAccountManagementSystem.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> UsernameExistsAsync(string username);
        Task CreateUserAsync(UserModel user);
        Task<UserModel?> GetUserByUsernameAsync(string username);
        Task<List<UserWithRolesModel>> GetAllUsersWithRolesAsync();
        Task<List<RoleModel>> GetAllRolesAsync();
    }
}
