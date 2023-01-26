using ApiExample.Database;
using ApiExample.Models;
using ApiExample.Interfaces;

namespace ApiExample.UseCases
{
    public class ClientUseCase : IClientUseCase
    {
        private readonly ApiExampleContext _apiExampleContext;
        public ClientUseCase(ApiExampleContext apiExampleContext)
        {
            _apiExampleContext = apiExampleContext;
        }

        public async Task<CreateCliente> Add(CreateCliente client)
        {
            await _apiExampleContext.AddClient(client);
            return client;
        }

        public async Task<Cliente?> Update(Cliente client)
        {
            var entity = await _apiExampleContext.Get(client.IdCliente);
            if(entity == null)
            {
                Console.WriteLine("No se encontro a un cliente con ese id");
                return null;
            }
            entity.First_Name = client.FirstName;
            entity.Last_Name = client.LastName;
            entity.Email = client.Email;
            entity.Phone = client.Phone;
            entity.Address = client.Address;
            await _apiExampleContext.UpdateClient(entity);
            return entity.ToModel();
        }
        public async Task<Cliente?> Delete(int id)
        {
            var entity = await _apiExampleContext.Get(id);
            if (entity == null)
            {
                Console.WriteLine("No se encontro a un cliente con ese id");
                return null;
            }
            await _apiExampleContext.DeleteClient(id);
            return entity.ToModel();
        }
    }
}
