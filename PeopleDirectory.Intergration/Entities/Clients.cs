using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleDirectory.Intergration.Entities
{
    public class Clients
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string MobileNumber { get; set; }
        public string Gender { get; set; }
        public string EmailAddress { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        public User? AdminCreator { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        public User? AdminUpdater { get; set; }


    }
}
