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

        public List<ClientEntity> GetAll()
        {
            List<ClientEntity> result = Client.FromSqlRaw(sql:"getAllClients()").ToList();
            return result;
        }

        public DbSet<ClientEntity> Client { get; set; }

        public async Task<ClientEntity> Get(long id)
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

            Client.FromSqlRaw("createClient("+entity+")");
            await SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Update(ClientEntity client)
        {
            Client.FromSqlRaw(sql:"updateClient("+client+")");
            await SaveChangesAsync();
            return true;
        } 

        public async Task<bool> Delete( long id)
        {
            ClientEntity entity = await Get(id);
            Client.FromSqlRaw(sql:"deleteClient("+entity+")");
            await SaveChangesAsync();
            return true;
        }
    }

}
