using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniAccountManagementSystem.Models;
using MiniAccountManagementSystem.Services.Accounts;

namespace MiniAccountManagementSystem.Pages.Accounts
{
    public class CreateModel : PageModel
    {
        private readonly IAccountService _accountService;

        public CreateModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty]
        public AccountModel Account { get; set; }

        public void OnGet()
        {
            Account = new AccountModel();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            // Add metadata for creation
            Account.Revision = 1;
            Account.AddedDate = DateTime.Now;
            Account.AddedBy = "admin";  // replace with actual logged-in user
            Account.AddedPc = 1;        // replace with actual client/machine ID

            await _accountService.AddAccountAsync(Account);

            return RedirectToPage("Index");
        }
    }
}
