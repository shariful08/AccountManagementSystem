using System.Collections.Generic;
using System.Threading.Tasks;
using MiniAccountManagementSystem.Models;

namespace MiniAccountManagementSystem.Services.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repository;

        public AccountService(IAccountRepository repository)
        {
            _repository = repository;
        }

        public Task<List<AccountModel>> GetAllAccountsAsync() => _repository.GetAllAccountsAsync();

        public Task AddAccountAsync(AccountModel account) => _repository.AddAccountAsync(account);
    }

}
