using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleDirectory.Application.DTO.Requests
{
    public class ClientDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string MobileNumber { get; set; }
        public string Gender { get; set; }
        public string EmailAddress { get; set; }
    }
}
