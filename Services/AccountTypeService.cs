using BankAPI.Data;
using BankAPI.Data.BankModels;

namespace BankAPI.Services;

public class AccountTypeService
{
    private readonly BankContext _context;
    public AccountTypeService(BankContext context)
    {
        _context = context;
    }

//GET
     public IEnumerable<AccountType> GetAll() 
    {
        return _context.AccountTypes.ToList();
    }

    public AccountType? GetById(int id)
    {
        return _context.AccountTypes.Find(id);
    }

}