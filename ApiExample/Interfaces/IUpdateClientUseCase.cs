using ApiExample.Models;

namespace ApiExample.Interfaces
{
    public interface IUpdateClientUseCase
    {
        Task<Client?> Execute(Client client);
    }
}
