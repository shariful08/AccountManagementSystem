using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiniAccountManagementSystem.Models;
using MiniAccountManagementSystem.Services.Accounts;
using MiniAccountManagementSystem.Services.Vouchers;
using MiniAccountManagementSystem.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniAccountManagementSystem.Pages.Vouchers
{
    public class CreateModel : PageModel
    {
        private readonly IVoucherService _voucherService;
        private readonly IAccountService _accountService;

        public CreateModel(IVoucherService voucherService, IAccountService accountService)
        {
            _voucherService = voucherService;
            _accountService = accountService;
        }

        [BindProperty]
        public VoucherEntryViewModel VoucherEntry { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            VoucherEntry = new VoucherEntryViewModel
            {
                VoucherMaster = new VoucherMaster(),
                VoucherDetails = new List<VoucherDetail>(),
                VoucherTypeList = new List<SelectListItem>
                {
                    new SelectListItem("Journal", "Journal"),
                    new SelectListItem("Payment", "Payment"),
                    new SelectListItem("Receipt", "Receipt")
                },
                AccountList = await _accountService.GetAllAccountsAsync()
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            bool success = await _voucherService.SaveVoucherAsync(VoucherEntry);
            if (success)
                return RedirectToPage("VoucherList");

            ModelState.AddModelError("", "Save failed.");
            return Page();
        }
    }
}
