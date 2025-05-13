
using MiniAccountManagementSystem.Models;

namespace MiniAccountManagementSystem.Services.Accounts
{

    public interface IAccountService
    {
        Task<List<AccountModel>> GetAllAccountsAsync();
        Task AddOrUpdateAccountAsync(AccountModel account);
    }

}
