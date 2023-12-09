using µMedlogr.core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using µMedlogr.core.Models;
using Microsoft.AspNetCore.Mvc;

namespace µMedlogr.Pages;
public class EventLogModel(HealthRecordService healthRecordService, PersonService personService, UserManager<AppUser> userManager, DrugService drugService) : PageModel {
    private readonly HealthRecordService _healthRecordService = healthRecordService;
    private readonly PersonService _personService = personService;
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly DrugService drugService = drugService;
    
    public List<Event> EventHistory { get; set; }
    public int HealthRecordId { get; set; }

    public async Task<ActionResult> OnGet(int healthRecordId) {
        if (healthRecordId == 0) {
            return BadRequest("Ett fel har inträffat");
        }
        //var appUserId = _userManager.GetUserId(User);
        EventHistory = _healthRecordService.GetAll().Result.Where(x => x.Id== healthRecordId).SelectMany(x => x.Events).ToList();

        return Page();
    }
}
