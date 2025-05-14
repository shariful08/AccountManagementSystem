using System.Collections.Generic;
using System.Threading.Tasks;
using MiniAccountManagementSystem.Models;

public interface IAccountRepository
{
    Task<List<AccountModel>> GetAllAccountsAsync();
    Task AddAccountAsync(AccountModel account);
    Task UpdateAccountAsync(AccountModel account);
}
