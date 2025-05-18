using Microsoft.AspNetCore.Mvc.Rendering;
using MiniAccountManagementSystem.Models;

namespace MiniAccountManagementSystem.ViewModels
{
    public class VoucherEntryViewModel
    {
        public VoucherMaster VoucherMaster { get; set; }
        public List<VoucherDetail> VoucherDetails { get; set; }
        public List<SelectListItem>? VoucherTypeList { get; set; } = null;
        public List<AccountModel>? AccountList { get; set; }
    }
}
