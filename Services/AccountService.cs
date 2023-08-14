using BankAPI.Data;
using BankAPI.Data.BankModels;

namespace BankAPI.Services;

public class AccountService
{
    private readonly BankContext _context;
    public AccountService(BankContext context)
    {
        _context = context;
    }

//GET
     public IEnumerable<Account> GetAll() 
    {
        return _context.Accounts.ToList();
    }

    public Account? GetById(int id)
    {
        return _context.Accounts.Find(id);
    }

//POST
    public Account Create(Account newAccount)
    {
        _context.Accounts.Add(newAccount); 
        _context.SaveChanges();

        return newAccount; 
    }

//PUT 
    public void Update(int id,Account account)
    {
        var existingAccount = GetById(id); 

        if(existingAccount is not null)
        {
            existingAccount.AccountType = account.AccountType;
            existingAccount.ClientId = account.ClientId;
            existingAccount.Balance = account.Balance;

            _context.SaveChanges();
        }
    }

//Delete
    public void Delete(int id)
    {
        var accountToDelete = GetById(id); 

        if(accountToDelete is not null) 
        {
            _context.Accounts.Remove(accountToDelete);
            _context.SaveChanges();
        }
    }
}
