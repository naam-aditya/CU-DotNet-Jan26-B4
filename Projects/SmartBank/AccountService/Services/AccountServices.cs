using AccountService.Dtos;
using AccountService.Exceptions;
using AccountService.Models;
using AccountService.Repositories;

namespace AccountService.Services;

public class AccountServices : IAccountService
{
    private readonly IAccountRepository _repository;
    public AccountServices(IAccountRepository repository) { _repository = repository; }

    public async Task<GetAccountResponse> AddAccount(CreateAccountRequest request)
    {
        if (request.InitialDeposit < 1000)
            throw new BadRequestException("Minimum initial balance: 1000");
        
        if (string.IsNullOrEmpty(request.Name) || string.IsNullOrWhiteSpace(request.Name))
            throw new BadRequestException("Provide valid name.");
        
        Account account = new()
        {
            Name = request.Name,
            Balance = request.InitialDeposit,
            CreatedAt = DateTime.Now
        };

        account = await _repository.AddAccount(account);

        account.AccountNumber = $"SB{account.CreatedAt.Year}{account.Id:D6}";
        await _repository.UpdateDatabaseAsync();

        return new GetAccountResponse()
        {
            Id = account.Id,
            Name = account.Name,
            AccountNumber = account.AccountNumber,
            Balance = account.Balance
        };
    }

    public async Task Deposit(Transaction transaction)
    {
        if (transaction.Amount < 0)
            throw new BadRequestException("Deposit amount should be greater than 0.");
        
        await _repository.UpdateAccount(transaction);
    }

    public async Task<GetAccountResponse> GetAccountById(int id)
    {
        var account = await _repository.GetAccountById(id)
            ?? throw new BadRequestException("Incorrect Account Id.");
        
        return new GetAccountResponse()
        {
            Id = account.Id,
            Name = account.Name,
            AccountNumber = account.AccountNumber,
            Balance = account.Balance
        };
    }

    public async Task<IEnumerable<GetAccountResponse>> GetAllccounts()
    {
        var accounts = await _repository.GetAccounts() ?? [];
                    
        return accounts.Select(
            a => new GetAccountResponse() 
            { 
                Id = a.Id,
                AccountNumber = a.AccountNumber,
                Name = a.Name,
                Balance = a.Balance,
            }
        );
    }


    public async Task RemoveAccountById(int id)
    {
        var result = await _repository.RemoveAccountById(id);
        if (result == false)
            throw new BadRequestException("Invalid Account Id.");
    }

    public async Task Withdraw(Transaction transaction)
    {
        var account = await _repository.GetAccountById(transaction.AccountId) 
            ?? throw new BadRequestException("Invalid Account Id.");
        
        if (account.Balance < transaction.Amount)
            throw new BadRequestException("Invalid withraw request.");
        
        transaction.Amount *= -1;
        await _repository.UpdateAccount(transaction);
    }
}