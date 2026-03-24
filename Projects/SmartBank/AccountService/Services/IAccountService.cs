using AccountService.Dtos;

namespace AccountService.Services;

public interface IAccountService
{
    Task<GetAccountResponse> AddAccount(CreateAccountRequest request);
    Task Deposit(Transaction transaction);
    Task Withdraw(Transaction transaction);
    Task<IEnumerable<GetAccountResponse>> GetAllccounts();
    Task<GetAccountResponse> GetAccountById(int id);
    Task RemoveAccountById(int id);
}