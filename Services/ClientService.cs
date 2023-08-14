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
     public IEnumerable<Client> GetAll() 
    {
        return _context.Clients.ToList(); 
    }

    public Client? GetById(int id) //Obtener el cliente por el id
    //Pedimos un objeto cliente o objeto nulo
    {
        return _context.Clients.Find(id);
    }

//POST
    public Client Create(Client newClient) //Retorna un cliente, y de parametro
    //acepta un cliente
    {
        _context.Clients.Add(newClient); //agregar al cliente
        _context.SaveChanges();

        return newClient; //retornamos ese nuevo cliente
    }

//PUT Actualizar un registro
//El metodo devuelve un void, de parametro acepta un cliente
    public void Update(int id,Client client)
    {
        var existingClient = GetById(id); //metodo de arriba

        if(existingClient is not null)
        {
            existingClient.Name = client.Name;
            existingClient.Phonenumber = client.Phonenumber;
            existingClient.Email = client.Email;

            _context.SaveChanges();
        }
    }

//Delete
    public void Delete(int id)
    {
        var clientToDelete = GetById(id); //Almenamos el id en una variable

        if(clientToDelete is not null) //si la variable no es nula, realiza el
        {                              //remove
            _context.Clients.Remove(clientToDelete);
            _context.SaveChanges();
        }
    }
}