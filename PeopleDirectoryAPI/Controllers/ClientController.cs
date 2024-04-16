using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleDirectory.Application;
using PeopleDirectory.Application.Bounderies;
using PeopleDirectory.Application.DTO.Requests;




namespace PeopleDirectoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientsService _clientService;
        private readonly INotificationService _notificationService;

        public ClientController(IClientsService clientService, INotificationService notificationService)
        {
            _clientService = clientService;
            _notificationService = notificationService;
        }

        [HttpGet("search")]
        public IActionResult SearchClients(string query)
        {
            var clients = _clientService.SearchClients(query);
            return Ok(clients);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            var clients = await _clientService.GetAllClients();
            return Ok(clients);
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(int id)
        {
            var client = await _clientService.GetClientById(id);
            return Ok(client);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateClient(ClientDto clientDto)
        {
            var createdClient = await _clientService.CreateClient(clientDto);
            await _notificationService.SendClientCreatedNotificationAsync(clientDto);
            return CreatedAtAction(nameof(GetClientById), new { id = createdClient.Id }, createdClient);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, ClientDto clientDto)
        {
            var updatedClient = await _clientService.UpdateClient(clientDto);
            var client = await _clientService.GetClientById(id);
            await _notificationService.SendClientUpdatedNotificationAsync(clientDto, client);

            if (updatedClient == null)
            {
                return NotFound();
            }
            return Ok(updatedClient);
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await _clientService.GetClientById(id);
            var deletedClient = await _clientService.DeleteClient(id);

            await _notificationService.SendClientDeletedNotificationAsync(client);

            if (!deletedClient)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
