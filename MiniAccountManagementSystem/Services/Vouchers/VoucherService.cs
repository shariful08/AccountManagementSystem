using MiniAccountManagementSystem.Models;
using MiniAccountManagementSystem.Repositories.Interfaces;
using MiniAccountManagementSystem.Services.Vouchers;

public class VoucherService : IVoucherService
{
    private readonly IVoucherRepository _repo;

    public VoucherService(IVoucherRepository repo)
    {
        _repo = repo;
    }

    public Task SaveVoucherAsync(List<VoucherModel> voucher)
    {
        return _repo.SaveVoucherAsync(voucher);
    }
}
