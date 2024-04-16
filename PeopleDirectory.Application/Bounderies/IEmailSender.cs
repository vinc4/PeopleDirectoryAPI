using FluentEmail.Core;
using RestSharp;
using static FParsec.ErrorMessage;

namespace PeopleDirectory.Application.Bounderies
{
    public interface IEmailSender
    {
        public Task<string> SendEmailAsync(string toEmail, string subject, string message);
    }
}