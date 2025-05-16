using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniAccountManagementSystem.Models;
using MiniAccountManagementSystem.Services.Accounts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniAccountManagementSystem.Pages.Accounts
{
    public class IndexModel : PageModel
    {
        private readonly IAccountService _accountService;
        public string ErrorMessage { get; set; }

        public IndexModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        //public List<AccountModel> Accounts { get; set; } = new();
        public List<AccountModel> AccountTree { get; set; } = new();

        public async Task OnGetAsync()
        {
            var Accounts = await _accountService.GetAllAccountsAsync();
            // Build tree
            AccountTree = BuildAccountTree(null, Accounts);
        }
        private List<AccountModel> BuildAccountTree(int? parentId, List<AccountModel> allAccounts)
        {
            return allAccounts
                .Where(a => a.ParentAccountId == parentId)
                .Select(a => {
                    a.Children = BuildAccountTree(a.AccountId, allAccounts);
                    return a;
                })
                .ToList();
        }

        // ✅ Delete Handler
        public async Task<IActionResult> OnPostDeleteAsync(int id)

        {
            try
            {
                if (id == null)
                {
                    // Optional: Log here too
                    ErrorMessage = "Something went wrong. Please contact support.";
                    return Page();
                }
                var account = new AccountModel
                {
                    AccountId = id,
                    UpdatedBy = "admin",        // or fetch from user
                    UpdatedDate = DateTime.Now,
                    UpdatedPc = 1
                };

                await _accountService.DeleteAccountAsync(account);
            }
            catch (Exception ex)
            {
                // Optional: log or return an error view
            }

            return RedirectToPage();
        }
    }
}
