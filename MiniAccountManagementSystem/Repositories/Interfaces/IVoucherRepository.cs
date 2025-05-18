using MiniAccountManagementSystem.Models;
using MiniAccountManagementSystem.ViewModels;

namespace MiniAccountManagementSystem.Repositories.Interfaces
{
    public interface IVoucherRepository
    {
        Task<bool> SaveVoucherAsync(VoucherEntryViewModel model);
    }
}
