using Microsoft.AspNetCore.Identity;

namespace µMedlogr.core.Models; 
public class AppUser : IdentityUser {

    public Person? Me { get; set; }
    public virtual ICollection<Person> PeopleInCareOf { get; set; }
    public AppUser() {
        PeopleInCareOf = new HashSet<Person>();
    }

}
