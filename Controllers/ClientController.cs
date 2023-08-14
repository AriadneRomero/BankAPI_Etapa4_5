using Microsoft.AspNetCore.Mvc;
using BankAPI.Services;
using BankAPI.Data.BankModels;

namespace BankAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase
{
    //Constructor, estructura base del Controller
    private readonly ClientService _service; 
    public ClientController(ClientService client)
    {
        _service = client;
    }

//llamadas GET
    [HttpGet] //Una solicitud get hara que se ejecute el método de abajo
                //Este metodo nos traera todos los registro
    public IEnumerable<Client> Get() //nos devuelve una conexion de objetos tipo client
    {
        return _service.GetAll(); //Nos devuelve la lista, toda la informacion
                                            //de clientes
    }

    [HttpGet("{id}")] //Aparte de escribir /cliente/id, escribira el id
    //Este metodo nos traera 1 cliente en especifico, proporcionando el id
    public ActionResult<Client> GetById(int id) 
    //ActionResult: Tener diferentes métodos que proporciona el ControllerBase
    {
        var client = _service.GetById(id); //Encontrar un registro en especifico

        if(client is null)
            return NotFound(); //devuelve un estatus 404 
        
        return client;
    }

    [HttpPost]
    //El objeto recibira un parametro tipo(clase) clientes
    public IActionResult Create(Client client) 
    {
        var newClient = _service.Create(client);

        return CreatedAtAction(nameof(GetById), new { id = client.Id}, newClient);
        //Este metodo nos devuelve un status 201
        //El controlador nos retornara el objeto que se acaba de crear
        //Los parametros que enviamos es la acción dentro del controlador

        //1ero se crea el objetos en el _context
        //2. Despues llamamos al metodo GetById, para eviarle devuelta al usuario es cliente
        //que se acaba de crear
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Client client) //Esto en la url
    {
        if(id != client.Id)
            return BadRequest(); //devuelve status 400

        var clientToUpdate = _service.GetById(id);

        if(clientToUpdate is not null)
        {
            _service.Update(id, client);
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
        var clientToDelete = _service.GetById(id);

        if(clientToDelete is not null)
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