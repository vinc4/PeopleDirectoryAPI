using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PeopleDirectory.Intergration.Entities
{
    public class User : IdentityUser
    {
        [Column(TypeName = "varchar(150)")]
        public string LastName { get; set; }
        public string Firstname { get; set; }
        public  string Email { get; set; }
        public string UpdatedBy { get; set; }
        public string CreatedBy { get; set; }
    }
}
