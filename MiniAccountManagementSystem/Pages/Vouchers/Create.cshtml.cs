using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using MiniAccountManagementSystem.Services.Vouchers;
using MiniAccountManagementSystem.ViewModels;

namespace MiniAccountManagementSystem.Pages.Vouchers
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public VoucherEntryViewModel VoucherEntry { get; set; }

        private readonly IVoucherService _voucherService;

        public CreateModel(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }

        public void OnGet() { }

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
