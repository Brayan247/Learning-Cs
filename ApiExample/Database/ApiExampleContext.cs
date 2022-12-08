using ApiExample.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ApiExample.Database
{
    public class ApiExampleContext : DbContext
    {
        public ApiExampleContext(DbContextOptions<ApiExampleContext> options)
            : base(options)
        {

        }

        public DbSet<ClientEntity> Client { get; set; }

        public async Task<ClientEntity?> Get(long id)
        {
            return await Client.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ClientEntity> Add(CreateClient createClient)
        {
            ClientEntity entity = new ClientEntity()
            {
                Id = null,
                First_Name = createClient.First_Name,
                Last_Name = createClient.Last_Name,
                Email = createClient.Email,
                Phone = createClient.Phone,
                Address = createClient.Address,
            };

            EntityEntry<ClientEntity> response = await Client.AddAsync(entity);
            await SaveChangesAsync();
            return await Get(response.Entity.Id ?? throw new Exception("No se ah podido guardar el cliente"));
        }

        public async Task<bool> Update(ClientEntity client)
        {
            Client.Update(client);
            await SaveChangesAsync();
            return true;
        } 

        public async Task<bool> Delete( long id)
        {
            ClientEntity entity = await Get(id);
            Client.Remove(entity);
            await SaveChangesAsync();
            return true;
        }
    }

    public class ClientEntity
    {
        public int? Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public Client ToModel()
        {
            return new Client()
            {
                Id = Id ?? throw new Exception("El id no puede ser nulo"),
                Firs_tName = First_Name,
                Last_Name = Last_Name,
                Email = Email,
                Phone = Phone,
                Address = Address,
            };
        }
    }
}
