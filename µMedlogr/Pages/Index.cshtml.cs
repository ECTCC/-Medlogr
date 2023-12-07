using µMedlogr.core;
using µMedlogr.core.Models;
using µMedlogr.core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace µMedlogr.Pages;

public class IndexModel(EntityManager entityManager, µMedlogrContext context, UserManager<AppUser> userManager) : PageModel
{
    private readonly EntityManager _entityManager = entityManager;
    private readonly µMedlogrContext _context = context;
    private readonly UserManager<AppUser> _userManager = userManager;
    public AppUser? MyUser { get; set; }
    public Person? Me { get; set; }
    public int HealthRecordId { get; set; }
    public List<Person> PeopleInCareOf { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        MyUser = await _userManager.GetUserAsync(User);
        if (MyUser is not null)
        {
            PeopleInCareOf = await _entityManager.GetJunctionData(MyUser);
            Me = _entityManager.GetUserPerson(MyUser.Id);
        }
        return Page();
    }
}
