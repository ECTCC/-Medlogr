using µMedlogr.core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using µMedlogr.core.Interfaces;

namespace µMedlogr.Pages;

public class IndexModel(UserManager<AppUser> userManager, IPersonService personService) : PageModel {

    public AppUser? MyUser { get; set; }
    public Person? Me { get; set; }

    public List<Person>? PeopleInCareOf { get; set; }

    public async Task<IActionResult> OnGetAsync() {
        MyUser = await userManager.GetUserAsync(User);
        if (MyUser is not null) {
            MyUser = await personService.GetAppUserWithRelationsById(MyUser.Id);
            PeopleInCareOf = [.. MyUser?.PeopleInCareOf];
            Me = MyUser?.Me;
        }
        return Page();
    }
}
