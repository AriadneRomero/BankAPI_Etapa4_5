using Microsoft.AspNetCore.Mvc;
using BankAPI.Services;
using BankAPI.Data.BankModels;

namespace BankAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly AccountService _service; 
    public AccountController(AccountService account)
    {
       _service = account;
    }

//llamadas GET
    [HttpGet]
    public IEnumerable<Account> Get() 
    {
        return _service.GetAll(); 
    }

    [HttpGet("{id}")] 
    public ActionResult<Account> GetById(int id) 
    {
        var account = _service.GetById(id); 

        if(account is null)
            return NotFound(); 
        
        return account;
    }

}