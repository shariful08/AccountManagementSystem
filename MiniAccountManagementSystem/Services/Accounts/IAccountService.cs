
using MiniAccountManagementSystem.Models;

namespace MiniAccountManagementSystem.Services.Accounts
{

    public interface IAccountService
    {
        Task<List<AccountModel>> GetAllAccountsAsync();
        Task AddAccountAsync(AccountModel account);
        Task UpdateAccountAsync(AccountModel account);
        Task DeleteAccountAsync(AccountModel account);
    }

}
