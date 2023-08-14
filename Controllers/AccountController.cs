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

    [HttpPost]
    public IActionResult Create(Account account) 
    {
        var newAccount = _service.Create(account);
  
        if(account.ClientId is null)
        {
            return BadRequest(); 
        }

        return CreatedAtAction(nameof(GetById), new { id = account.Id}, newAccount);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Account account)
    {
        if(id != account.Id)
            return BadRequest(); //devuelve status 400

        var accountToUpdate = _service.GetById(id);

        if(accountToUpdate is not null)
        {
            _service.Update(id, account);
            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var accountToDelete = _service.GetById(id);

        if(accountToDelete is not null)
        {
            _service.Delete(id);
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }

}