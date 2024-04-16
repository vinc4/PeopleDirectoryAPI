using RestSharp.Authenticators;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;
using PeopleDirectory.Application.Bounderies;
using System.Net.Http.Headers;



namespace PeopleDirectory.Application
{
    public class EmailSender : IEmailSender
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _mailgunBaseUrl;
        private readonly string _mailgunApiKey;
        private readonly string _mailgunApiUsername;

        public EmailSender(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _mailgunBaseUrl = "https://api.mailgun.net/v3/";
            _mailgunApiKey = "e70447f4eeafc648b4724ff67a7476a8-19806d14-7d20646a";
            _mailgunApiUsername = "vincentmap@gmail.com";
        }

        public async Task<string> SendEmailAsync(string toEmail, string subject, string message)
        {
            var client = _httpClientFactory.CreateClient();

       
            var base64String = Convert.ToBase64String(Encoding.ASCII.GetBytes($@"{_mailgunApiUsername}:{_mailgunApiKey}"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64String);

            var postData = new MultipartFormDataContent();
            postData.Add(new StringContent("vincentmap@gmail.com"), "from"); 
            postData.Add(new StringContent(toEmail), "to");
            postData.Add(new StringContent(subject), "subject");
            postData.Add(new StringContent(message), "text"); 


            var domainName = "sandbox9e13a518f7b84071b528300398441193.mailgun.org";
            var request = await client.PostAsync(_mailgunBaseUrl + domainName + "/messages", postData);
            var response = await request.Content.ReadAsStringAsync();

            return response;
        }
    }

}