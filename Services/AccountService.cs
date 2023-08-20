using Microsoft.EntityFrameworkCore;
using BankAPI.Data;
using BankAPI.Data.BankModels;
using TestBankAPI.DTOS;

namespace BankAPI.Services;

public class AccountService
{
    private readonly BankContext _context;
    public AccountService(BankContext context)
    {
        _context = context;
    }

//GET
     public async Task<IEnumerable<Account>> GetAll() 
    {
        return await _context.Accounts.ToListAsync();
    }

    public async Task<Account?> GetById(int id)
    {
        return await _context.Accounts.FindAsync(id);
    }

//POST

    public async Task<Account> Create(AccountDTO newAccountDTO)
    {
        var newAccount = new Account();

        newAccount.AccountType = newAccountDTO.AccountType;
        newAccount.ClientId = newAccountDTO.ClientId;
        newAccount.Balance = newAccountDTO.Balance;

        _context.Accounts.Add(newAccount); 
        await _context.SaveChangesAsync();

        return newAccount; 
    }

//PUT 
    public async Task Update(AccountDTO account)
    {
        var existingAccount = await GetById(account.Id); 

        if(existingAccount is not null)
        {
            existingAccount.AccountType = account.AccountType;
            existingAccount.ClientId = account.ClientId;
            existingAccount.Balance = account.Balance;

            await _context.SaveChangesAsync();
        }
    }

//Delete
    public async Task Delete(int id)
    {
        var accountToDelete = await GetById(id); 

        if(accountToDelete is not null) 
        {
            _context.Accounts.Remove(accountToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
