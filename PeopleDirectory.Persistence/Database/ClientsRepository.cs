using Microsoft
    .EntityFrameworkCore;
using PeopleDirectory.Intergration.Entities;
using PeopleDirectory.Intergration.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleDirectory.Persistence.Database
{
    public class ClientsRepository : IClientsRepository
    {
        private readonly ApplicationDbContext _dataContext;

        public ClientsRepository(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }


        public async Task<Clients> CreateUserAsync(Clients user)
        {
            await _dataContext.Clients.AddAsync(user);
            var changes = await _dataContext.SaveChangesAsync();
            if (changes > 0)
            {
                return await ReadClientAsync(user.Id);
            } 
            else throw new ApplicationException($"Cannot add user: {user.Id} to database");
        }


        public async Task<bool> UpdateClientAsync(Clients user)
        {
            _dataContext.Clients.Update(user);
            var changes = await _dataContext.SaveChangesAsync();
            return changes > 0;

        }
        public async Task<bool> DeleteClientAsync(int id)
        {
            var client = await ReadClientAsync(id);
            if (client != null)
            {
                _dataContext.Clients.Remove(client);
                var changes = await _dataContext.SaveChangesAsync();
                return changes > 0;

            }
            return false;
        }




        public async Task<IEnumerable<Clients>> ReadClientsAsync()
        {
            var queryable = _dataContext.Clients.AsQueryable();
            return await queryable.ToListAsync();
        }

        public async Task<IEnumerable<Clients>> SearchClients(string query)
        {

            var clients = await _dataContext.Clients
            .Where(c => EF.Functions.Like(c.Name, $"%{query}%") || EF.Functions.Like(c.Surname, $"%{query}%"))
            .ToListAsync();

            return clients.ToList();
        }
        

        public async Task<Clients> ReadClientAsync(int id)
        {
            var queryable = _dataContext.Clients.AsQueryable();
            return await queryable.SingleOrDefaultAsync<Clients>(x => x.Id == id);
        }


    }
}
