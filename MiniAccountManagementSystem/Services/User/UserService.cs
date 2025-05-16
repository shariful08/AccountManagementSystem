using Microsoft.AspNetCore.Mvc.Rendering;
using MiniAccountManagementSystem.Models;
using MiniAccountManagementSystem.Repositories;
using MiniAccountManagementSystem.Repositories.Interfaces;

namespace MiniAccountManagementSystem.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public Task<bool> UsernameExistsAsync(string username)
        {
            return _repo.UsernameExistsAsync(username);
        }

        public Task CreateUserAsync(UserModel user)
        {
            return _repo.CreateUserAsync(user);
        }
        public Task<UserModel?> GetUserByUsernameAsync(string username)
        {
            return _repo.GetUserByUsernameAsync(username);
        }
        public Task<List<UserWithRolesModel>> GetAllUsersWithRolesAsync()
        {
            return _repo.GetAllUsersWithRolesAsync();
        }

        public async Task<List<SelectListItem>> GetRolesForDropdownAsync()
        {
            var roles = await _repo.GetAllRolesAsync();
            return roles.Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.RoleName
            }).ToList();
        }

    }
}
