using PeopleDirectory.Application.DTO.Requests;
using PeopleDirectory.Intergration.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleDirectory.Application.Bounderies
{
    public interface IClientsService
    {
        Task<Clients> GetClientById(int id);
        Task<List<Clients>> GetAllClients();
        Task<bool> UpdateClient(ClientDto user);
        Task<bool> DeleteClient(int id);
        Task<Clients> CreateClient(ClientDto user);
        Task<List<Clients>> SearchClients(string query);

    }
}
