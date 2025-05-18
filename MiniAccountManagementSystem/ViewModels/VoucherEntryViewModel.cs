using MiniAccountManagementSystem.Models;

namespace MiniAccountManagementSystem.ViewModels
{
    public class VoucherEntryViewModel
    {
        public VoucherMaster VoucherMaster { get; set; }
        public List<VoucherDetail> VoucherDetails { get; set; }
    }
}
