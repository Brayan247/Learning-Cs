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
        private readonly IUpdateClientUseCase _updateClientUseCase;

        public ClientController(ApiExampleContext apiExampleContext, IUpdateClientUseCase updateClientUseCase)
        {
            _apiExampleContext = apiExampleContext;
            _updateClientUseCase = updateClientUseCase;
        }

        // Get all clients
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetClients()
        {
            var result = _apiExampleContext.GetAll();
            return new OkObjectResult(result);
        }

        // Get client by Id
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Client))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetClient(long id)
        {
            ClientEntity result = await _apiExampleContext.Get(id);
            return new OkObjectResult(result.ToModel());
        }

        // Create client
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Client))]
        public async Task<IActionResult> CreateClient(CreateClient client)
        {
            ClientEntity result =  await _apiExampleContext.Add(client);
            return new CreatedResult($"https://localhost:7212/api/client/{result.Id}", null);
        }

        // Edit client
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Client))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EditClient(Client client)
        {
            Client? result = await _updateClientUseCase.Execute(client);
            if(result == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(result);
        }

        // Delete client
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteClient(long id)
        {
            var result = await _apiExampleContext.Delete(id);
            return new OkObjectResult(result);
        }
    }

}
