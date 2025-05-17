using MiniAccountManagementSystem.Models;

namespace MiniAccountManagementSystem.Services.Vouchers
{
    public interface IVoucherService
    {
        Task SaveVoucherAsync(List<VoucherModel> voucher);
    }
}
