using µMedlogr.core;
using µMedlogr.core.Models;
using µMedlogr.core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace µMedlogr.Pages;

public class IndexModel(UserManager<AppUser> userManager, PersonService personService) : PageModel {
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly PersonService _personService = personService;

    public AppUser? MyUser { get; set; }
    public Person? Me { get; set; }

    public List<Person> PeopleInCareOf { get; set; }

    public async Task<IActionResult> OnGetAsync() {
        MyUser = await _userManager.GetUserAsync(User);
        if (MyUser is not null) {
            MyUser = await _personService.GetAppUserWithRelationsById(MyUser.Id);
            PeopleInCareOf = [.. MyUser?.PeopleInCareOf];
            Me = MyUser?.Me;
        }
        return Page();
    }
}
