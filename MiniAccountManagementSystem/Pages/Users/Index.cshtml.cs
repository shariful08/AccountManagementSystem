using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniAccountManagementSystem.Models;
using MiniAccountManagementSystem.Services.Users;

namespace MiniAccountManagementSystem.Pages.Users
{
    //[Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;

        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        public List<UserWithRolesModel> Users { get; set; } = new();

        public async Task OnGetAsync()
        {
            Users = await _userService.GetAllUsersWithRolesAsync();
        }
    }
}
