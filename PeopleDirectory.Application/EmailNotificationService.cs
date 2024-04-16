using Microsoft.Extensions.Configuration;
using PeopleDirectory.Application.Bounderies;
using PeopleDirectory.Application.DTO.Requests;
using PeopleDirectory.Intergration.Entities;



namespace PeopleDirectory.Application
{
    public class EmailNotificationService : INotificationService
    {
      
        private readonly IEmailSender _emailSender;
        private readonly string toEmail = "vincentmap@gmail.com";

        public EmailNotificationService(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task SendClientCreatedNotificationAsync(ClientDto client)
        {

            
            var subject = "New Client Created in People Directory";

            var message = $"A new client has been created: \n\n" +
                           $"Name: {client.Name} {client.Surname} \n" +
                           $"Email: {client.EmailAddress} \n" +
                           $"Mobile Number: {client.MobileNumber} \n" +
                           $"Location: {client.City}, {client.Country} \n" +
                           $"Gender: {client.Gender} \n";

            await _emailSender.SendEmailAsync(toEmail, subject, message);
        }

        public async Task SendClientUpdatedNotificationAsync(ClientDto oldClient, Clients newClient)
        {

            var subject = "Client Updated in People Directory";

            var message = $"Client details have been updated: \n\n" +
                           $"Name (Old): {oldClient.Name} {oldClient.Surname} \n" + 
                           $"Name (New): {newClient.Name} {newClient.Surname} \n" + 
                           $"Email (Old): {oldClient.EmailAddress} \n" +
                           $"Email (New): {newClient.EmailAddress} \n" +
                           $"Mobile Number (Old): {oldClient.MobileNumber} \n" +
                           $"Mobile Number (New): {newClient.MobileNumber} \n" +
                           $"Location (Old): {oldClient.City}, {oldClient.Country} \n" +
                           $"Location (New): {newClient.City}, {newClient.Country} \n" +
                           $"Gender (Old): {oldClient.Gender} \n" +
                           $"Gender (New): {newClient.Gender} \n";

            await _emailSender.SendEmailAsync( toEmail, subject, message);
        }

        public async Task SendClientDeletedNotificationAsync(Clients client)
        {
            if (client == null)
            {
                return;
            }


            var subject = "Client Deleted from People Directory";

            var message = $"Client deleted: \n\n" +
                           $"Name: {client.Name} {client.Surname} \n" +
                           $"Email: {client.EmailAddress} \n" +
                           $"Mobile Number: {client.MobileNumber} \n" +
                           $"Location: {client.City}, {client.Country} \n" +
                           $"Gender: {client.Gender} \n";

            await _emailSender.SendEmailAsync( toEmail, subject, message);
        }
    }

}

