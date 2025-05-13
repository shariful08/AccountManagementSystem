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

        public IndexModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public List<AccountModel> Accounts { get; set; }

        public async Task OnGetAsync()
        {
            Accounts = await _accountService.GetAllAccountsAsync();
        }
    }
}
