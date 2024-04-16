using PeopleDirectory.Application.DTO.Requests;
using PeopleDirectory.Intergration.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleDirectory.Application.Bounderies
{
    public interface INotificationService
    {
        Task SendClientCreatedNotificationAsync(ClientDto client);
        Task SendClientUpdatedNotificationAsync(ClientDto oldClient, Clients newClient);
        Task SendClientDeletedNotificationAsync(Clients client);
    }

}
