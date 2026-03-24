using AccountService.Dtos;
using AccountService.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly AppDbContext _context;

    public AccountRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Account> AddAccount(Account account)
    {
        _context.Account.Add(account);
        await _context.SaveChangesAsync();

        return account;
    }

    public async Task<Account?> GetAccountById(int id) =>
        await _context.Account.FindAsync(id);

    public async Task<IEnumerable<Account>?> GetAccounts() =>
        await _context.Account.ToListAsync();

    public async Task<bool> RemoveAccount(Account account)
    {
        var temp = await _context.Account.FindAsync(account.Id);
        if (account == null)
            return false;
        
        _context.Account.Remove(account);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveAccountById(int id)
    {
        var account = await _context.Account.FindAsync(id);
        if (account == null)
            return false;
        
        _context.Account.Remove(account);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateAccount(Transaction transaction)
    {
        var account = await GetAccountById(transaction.AccountId);
        if (account == null)
            return false;
        
        account.Balance += transaction.Amount;
        _context.Entry(account).State = EntityState.Modified;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateAccountName(UpdateAccountNameRequest request)
    {
        var account = await GetAccountById(request.AccountId);
        if (account == null)
            return false;
        
        account.Name = request.Name;
        _context.Entry(account).State = EntityState.Modified;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task UpdateDatabaseAsync()
    {
        await _context.SaveChangesAsync();
    }
}