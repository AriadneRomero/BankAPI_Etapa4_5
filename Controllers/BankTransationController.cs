using Microsoft.AspNetCore.Mvc;
using BankAPI.Services;
using BankAPI.Data.BankModels;
using TestBankAPI.DTOS;
using Microsoft.AspNetCore.Authorization;

namespace BankAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BankTransactionController : ControllerBase
{
    private readonly BankTransactionService _bankTransactionService;
    private readonly ClientService _clientService;
    private readonly AccountService _accountService;
    public BankTransactionController(BankTransactionService bankTransactionService, ClientService clientService, AccountService accountService)
    {
        _bankTransactionService = bankTransactionService;
        _clientService = clientService;
        _accountService = accountService;

    }


    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAccount(int id) 
    {
        var accountToDelete = await _bankTransactionService.GetAccountById(id);

        if (accountToDelete is not null && accountToDelete.Balance == 0)
        {
            await _bankTransactionService.DeleteAccount(accountToDelete);
            return Ok();
        } else
        {
            return BadRequest(new {message = "La cuenta no puede ser borrada"});
        }
    }

    
    
}