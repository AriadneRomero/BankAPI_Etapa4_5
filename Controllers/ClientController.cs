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
    public async Task<IEnumerable<Client>> Get() //nos devuelve una conexion de objetos tipo client
    {
        return await _service.GetAll(); //Nos devuelve la lista, toda la informacion
                                            //de clientes
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Client>> GetById(int id) 
    //ActionResult: Tener diferentes métodos que proporciona el ControllerBase
    {
        var client = await _service.GetById(id); 

        if(client is null)
            return ClientNotFound(id);
            //Enviamos un objeto como parametro del notfound
            //Es un objeto anonimo, que solo tiene de propiedad message, para mostrar mensajes
        
        return client;
    }

    [HttpPost]
    //El objeto recibira un parametro tipo(clase) clientes
    public async Task<IActionResult> Create(Client client) 
    {
        var newClient = await _service.Create(client);

        return CreatedAtAction(nameof(GetById), new { id = client.Id}, newClient);
        //Este metodo nos devuelve un status 201
        //El controlador nos retornara el objeto que se acaba de crear
        //Los parametros que enviamos es la acción dentro del controlador
    }
        //1ero se crea el objetos en el _context
        //2. Despues llamamos al metodo GetById, para eviarle devuelta al usuario es cliente
        //que se acaba de crear
    

    [HttpPut("{id}")]
    public async Task <IActionResult> Update(int id, Client client) //Esto en la url
    {
        if(id != client.Id)
            return BadRequest(new { message = $"El ID({id}) de la URL no coincide con el ID({client.Id}) del cuerpo de la solicitud."}); 

        var clientToUpdate = await _service.GetById(id);

        if(clientToUpdate is not null)
        {
            await _service.Update(id, client);
            return NoContent();
        }
        else
        {
            return ClientNotFound(id);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var clientToDelete = await _service.GetById(id);

        if(clientToDelete is not null)
        {
            await _service.Delete(id);
            return Ok();
        }
        else
        {
            return ClientNotFound(id);
        }
    }

    public NotFoundObjectResult ClientNotFound (int id)
    {
        return NotFound (new { message = $"El cliente con ID = {id} no existe."});
    }
}