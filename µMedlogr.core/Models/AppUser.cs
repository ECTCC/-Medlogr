using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace µMedlogr.core.Models
{
    internal class AppUser: IdentityUser
    {
        public Person Me {  get; set; }
        public virtual ICollection<Person> PeopleInCareOf  { get; set; }
        internal AppUser()
        { 
            PeopleInCareOf = new HashSet<Person>();
        }

    }
}
