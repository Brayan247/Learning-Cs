using ApiExample.Database;
using ApiExample.Models;
using ApiExample.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiExample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : Controller
    {
        private readonly ApiExampleContext _apiExampleContext;
        private readonly IClientUseCase _clientUseCase;

        public ClientController(ApiExampleContext apiExampleContext, IClientUseCase updateClientUseCase)
        {
            _apiExampleContext = apiExampleContext;
            _clientUseCase = updateClientUseCase;
        }

        // Get all clients
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetClients()
        {
            var result = _apiExampleContext.GetAll();
            return new OkObjectResult(result);
        }

        // Get client by Idcliente
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Cliente))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetClient(int id)
        {
            ClienteEntity result = await _apiExampleContext.Get(id);
            return new OkObjectResult(result.ToModel());
        }

        // Create client
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateCliente))]
        public async Task<IActionResult> CreateClient(CreateCliente client)
        {
            var result =  await _clientUseCase.Add(client);
            if (result == null)
            {
                return new EmptyResult();
            }
            return new OkObjectResult(result);
        }

        // Edit client
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Cliente))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EditClient(Cliente client)
        {
            var result = await _clientUseCase.Update(client);
            if(result == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(result);
        }

        // Delete client
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Cliente))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var result = await _clientUseCase.Delete(id);
            if (result == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(result);
        }
    }

}
