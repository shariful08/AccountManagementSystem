using MiniAccountManagementSystem.Models;
using MiniAccountManagementSystem.ViewModels;

namespace MiniAccountManagementSystem.Services.Vouchers
{
    public interface IVoucherService
    {
        Task<bool> SaveVoucherAsync(VoucherEntryViewModel model);
        Task<List<VoucherMaster>> GetAllVouchersAsync();

    }
}
