using Microsoft.AspNetCore.Mvc;
using BankAPI.Services;
using BankAPI.Data.BankModels;

namespace BankAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountTypeController : ControllerBase
{
    private readonly AccountTypeService _service; 
    public AccountTypeController(AccountTypeService accountType)
    {
       _service = accountType;
    }

//llamadas GET
    [HttpGet]
    public IEnumerable<AccountType> Get() 
    {
        return _service.GetAll(); 
    }

    [HttpGet("{id}")] 
    public ActionResult<AccountType> GetById(int id) 
    {
        var accountType = _service.GetById(id); 

        if(accountType is null)
            return NotFound(); 
        
        return accountType;
    }
}