using MiniAccountManagementSystem.Models;

namespace MiniAccountManagementSystem.Repositories.Interfaces
{
    public interface IVoucherRepository
    {
        Task SaveVoucherAsync(List<VoucherModel> voucher);
    }
}
