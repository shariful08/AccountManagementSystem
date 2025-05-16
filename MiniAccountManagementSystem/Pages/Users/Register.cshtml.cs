using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniAccountManagementSystem.Helper;
using MiniAccountManagementSystem.Models;
using MiniAccountManagementSystem.Services.Users;
using System.ComponentModel.DataAnnotations;

namespace MiniAccountManagementSystem.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IUserService _userService;

        public RegisterModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty, Required]
        public string Username { get; set; }

        [BindProperty, Required]
        public string Password { get; set; }

        [BindProperty]
        public string FullName { get; set; }

        [BindProperty, Required(ErrorMessage = "Please select a role.")]
        public int? SelectedRoleId { get; set; }

        public List<SelectListItem> RoleList { get; set; }

        public string Message { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            RoleList = await _userService.GetRolesForDropdownAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            RoleList = await _userService.GetRolesForDropdownAsync();

            if (!ModelState.IsValid)
            {
                Message = "All required fields must be filled.";
                return Page();
            }

            if (await _userService.UsernameExistsAsync(Username))
            {
                Message = "Username already exists.";
                return Page();
            }

            var user = new UserModel
            {
                Username = Username,
                PasswordHash = PasswordHelper.Hash(Password),
                FullName = FullName,
                SelectedRoleId = SelectedRoleId
            };

            await _userService.CreateUserAsync(user);
            Message = "User registered successfully.";
            return Page();
        }
    }
}
