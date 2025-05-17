using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniAccountManagementSystem.Models;
using MiniAccountManagementSystem.Services;
using MiniAccountManagementSystem.Services.Vouchers;

public class CreateModel : PageModel
{
    private readonly IVoucherService _voucherService;

    public CreateModel(IVoucherService voucherService)
    {
        _voucherService = voucherService;
    }

    [BindProperty]
    public List<VoucherModel> VoucherEntries { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        await _voucherService.SaveVoucherAsync(VoucherEntries);

        TempData["Success"] = "Vouchers saved successfully!";
        return RedirectToPage("VoucherList");
    }
}
