using ApiExample.Models;

namespace ApiExample.Interfaces
{
    public interface IClientUseCase
    {
        Task<CreateCliente?> Add(CreateCliente client);
        Task<Cliente?> Update(Cliente client);
        Task<Cliente?> Delete(int id);
    }
}
