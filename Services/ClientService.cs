using Microsoft.EntityFrameworkCore;
using BankAPI.Data;
using BankAPI.Data.BankModels;

namespace BankAPI.Services;

public class ClientService
{
    private readonly BankContext _context;
    public ClientService(BankContext context)
    {
        _context = context;
    }

//GET
     public async Task<IEnumerable<Client>> GetAll() 
    {
        return await _context.Clients.ToListAsync(); 
    }

    public async Task<Client?> GetById(int id) //Obtener el cliente por el id
    //Pedimos un objeto cliente o objeto nulo
    {
        return await _context.Clients.FindAsync(id);
    }

//POST
    public async Task<Client> Create(Client newClient) //Retorna un cliente, y de parametro
    //acepta un cliente
    {
        _context.Clients.Add(newClient); //agregar al cliente
        await _context.SaveChangesAsync();

        return newClient; //retornamos ese nuevo cliente
    }

//PUT Actualizar un registro
//El metodo devuelve un void, de parametro acepta un cliente
    public async Task Update(int id,Client client)
    {
        var existingClient = await GetById(id); //metodo de arriba

        if(existingClient is not null)
        {
            existingClient.Name = client.Name;
            existingClient.Phonenumber = client.Phonenumber;
            existingClient.Email = client.Email;

            await _context.SaveChangesAsync();
        }
    }

//Delete
    public async Task Delete(int id)
    {
        var clientToDelete = await GetById(id); //Almenamos el id en una variable

        if(clientToDelete is not null) //si la variable no es nula, realiza el
        {                              //remove
            _context.Clients.Remove(clientToDelete);
            await _context.SaveChangesAsync();
        }
    }
}