using ApiExample.Database;
using ApiExample.Models;

namespace ApiExample.UseCases
{
    public interface IUpdateClientUseCase
    {
        Task<Client?> Execute(Client client);
    }
    public class UpdateClientUseCase : IUpdateClientUseCase
    {
        private readonly ApiExampleContext _apiExampleContext;
        public UpdateClientUseCase(ApiExampleContext apiExampleContext)
        {
            _apiExampleContext = apiExampleContext;
        }

        public async Task<Client?> Execute(Client client)
        {
            var entity = await _apiExampleContext.Get(client.Id);
            if(entity == null)
            {
                return null;
            }
            entity.First_Name = client.Firs_tName;
            entity.Last_Name = client.Last_Name;
            entity.Email = client.Email;
            entity.Phone = client.Phone;
            entity.Address = client.Address;
            await _apiExampleContext.Update(entity);
            return entity.ToModel();
        }
    }
}
