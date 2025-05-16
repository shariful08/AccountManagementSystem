using Microsoft.AspNetCore.Mvc.Rendering;
using MiniAccountManagementSystem.Models;

namespace MiniAccountManagementSystem.Services.Users
{
    public interface IUserService
    {
        Task<bool> UsernameExistsAsync(string username);
        Task CreateUserAsync(UserModel user);
        Task<UserModel?> GetUserByUsernameAsync(string username);

        Task<List<UserWithRolesModel>> GetAllUsersWithRolesAsync();
        Task<List<SelectListItem>> GetRolesForDropdownAsync();

    }
}
