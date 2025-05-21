using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniAccountManagementSystem.Services.Vouchers;
using MiniAccountManagementSystem.ViewModels;

namespace MiniAccountManagementSystem.Pages.Vouchers
{
    public class VoucherListModel : PageModel
    {
        private readonly IVoucherService _voucherService;

        public VoucherListModel(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }

        public List<VoucherListItemViewModel> Vouchers { get; set; } = new();

        public async Task OnGetAsync()
        {
            var data = await _voucherService.GetAllVouchersAsync();

            Vouchers = data.Select(v => new VoucherListItemViewModel
            {
                VoucherMasterId = (int)v.VoucherMasterId,
                VoucherNo = v.VoucherNo,
                VoucherDate = (DateTime)v.VoucherDate,
                VoucherType = v.VoucherType,
                Narration = v.Narration,
                Remarks = v.Remarks
            }).ToList();
        }
    }
}
