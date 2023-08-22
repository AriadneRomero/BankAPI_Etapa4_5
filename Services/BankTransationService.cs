using Microsoft.EntityFrameworkCore;
using BankAPI.Data;
using BankAPI.Data.BankModels;
using TestBankAPI.DTOS;

namespace BankAPI.Services;

public class BankTransactionService
{
    private readonly BankContext _context;
    public BankTransactionService(BankContext context)
    {
        _context = context;
    }
    public async Task<AccountDtoOut?> GetAccountsClients(int clientId)
    {
        return await _context.Accounts.
            Where(account => account.ClientId == clientId).
            Select(a => new AccountDtoOut
            {
                Id = a.Id,
                AccountName = a.AccountTypeNavigation.Name,
                ClientName = a.Client == null ? "" : a.Client.Name!,
                Balance = a.Balance,
                RegDate = a.RegDate
            }).SingleOrDefaultAsync();
    }

    public async Task<Account?> GetAccountById(int idAccount)
    {
        return await _context.Accounts.FindAsync(idAccount);            
    }

    public async Task DeleteAccount(Account account)
    {
        _context.Accounts.Remove(account);
        await _context.SaveChangesAsync();
    }
}