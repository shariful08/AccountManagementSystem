using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniAccountManagementSystem.Models;
using MiniAccountManagementSystem.Services.Accounts;

namespace MiniAccountManagementSystem.Pages.Accounts
{
    public class CreateModel : PageModel
    {
        public string ErrorMessage { get; set; }
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
        // GET for Edit
        public async Task<IActionResult> OnGetEditAsync(int id)
        {
            var accounts = await _accountService.GetAllAccountsAsync();
            Account = accounts.FirstOrDefault(a => a.AccountId == id);
            if (Account == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                // Add metadata for creation
                Account.Revision = 1;
                Account.AddedDate = DateTime.Now;
                Account.AddedBy = "admin";  // replace with actual logged-in user
                Account.AddedPc = 1;        // replace with actual client/machine ID

                await _accountService.AddAccountAsync(Account);

                return RedirectToPage("Index");
            }
            catch (Exception ex)
            {
                // Optional: Log here too
                ErrorMessage = "Something went wrong. Please contact support.";
                return Page();
            }
            

            
        }
        // POST for Update
        public async Task<IActionResult> OnPostUpdateAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                    return Page();

                Account.UpdatedDate = DateTime.Now;
                Account.UpdatedBy = "admin";
                Account.UpdatedPc = 1;

                await _accountService.UpdateAccountAsync(Account);

                return RedirectToPage("Index");
            }
            catch (Exception ex)
            {
                // Optional: Log here too
                ErrorMessage = "Something went wrong. Please contact support.";
                return Page();
            }
            
        }
        
    }
}
