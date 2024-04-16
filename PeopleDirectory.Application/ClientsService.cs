using AutoMapper;
using PeopleDirectory.Application.Bounderies;
using PeopleDirectory.Application.DTO.Requests;
using PeopleDirectory.Intergration.Entities;
using PeopleDirectory.Intergration.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleDirectory.Application
{
    public class ClientsService : IClientsService
    {
        private readonly IMapper _mapper;
        private readonly IClientsRepository _clientsRepository;

        public ClientsService(IMapper mapper , IClientsRepository clientsRepository)
        { 
            _mapper = mapper;
            _clientsRepository = clientsRepository;
        }

        public async Task<Clients> CreateClient(ClientDto user)
        {
            var userFilter = _mapper.Map<Clients>(user);
            var client = await _clientsRepository.CreateUserAsync(userFilter);

            return client;
        }

        public Task<bool> DeleteClient(int id)
        {
            var deleteClient = _clientsRepository.DeleteClientAsync(id);
            return deleteClient;
        }

        public async Task<List<Clients>> SearchClients(string query)
        {
            var queryable = await _clientsRepository.SearchClients( query);
            return queryable.ToList();
        }

        public async Task<List<Clients>> GetAllClients()
        {
            var queryable = await _clientsRepository.ReadClientsAsync();
            return queryable.ToList();
        }

        public async Task<Clients> GetClientById(int id)
        {
            var client = await _clientsRepository.ReadClientAsync(id);
            return client;
        }

        public async Task<bool> UpdateClient(ClientDto client)
        {
            var _client = _mapper.Map<Clients>(client);
            var clientUpdated = await _clientsRepository.UpdateClientAsync(_client);
            return clientUpdated;
        }
    }
}
