using PeopleDirectory.Intergration.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleDirectory.Intergration.RepositoryInterfaces
{
    public interface IClientsRepository
    {
        Task<Clients> CreateUserAsync(Clients user);
        Task<IEnumerable<Clients>> ReadClientsAsync();
        Task<Clients> ReadClientAsync(int id);
        Task<bool> UpdateClientAsync(Clients user);
        Task<bool> DeleteClientAsync(int id);
        Task<IEnumerable<Clients>> SearchClients(string query);
    }
}
