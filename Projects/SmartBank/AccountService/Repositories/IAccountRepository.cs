using AccountService.Dtos;
using AccountService.Models;

namespace AccountService.Repositories;

public interface IAccountRepository
{
    Task<Account> AddAccount(Account account);
    Task<Account?> GetAccountById(int id);
    Task<IEnumerable<Account>?> GetAccounts();
    Task<bool> RemoveAccountById(int id);
    Task<bool> RemoveAccount(Account account);
    Task<bool> UpdateAccount(Transaction transaction);
    Task<bool> UpdateAccountName(UpdateAccountNameRequest request);
    Task UpdateDatabaseAsync();
}