using MiniAccountManagementSystem.Models;
using MiniAccountManagementSystem.Repositories.Interfaces;
using MiniAccountManagementSystem.Services.Vouchers;
using MiniAccountManagementSystem.ViewModels;

public class VoucherService : IVoucherService
{
    private readonly IVoucherRepository _repository;

    public VoucherService(IVoucherRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> SaveVoucherAsync(VoucherEntryViewModel model)
    {
        return await _repository.SaveVoucherAsync(model);
    }
}
