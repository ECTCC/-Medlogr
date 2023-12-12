using µMedlogr.core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using µMedlogr.core.Models;
using Microsoft.AspNetCore.Mvc;

namespace µMedlogr.Pages;
public class EventLogModel(HealthRecordService healthRecordService) : PageModel {

    public List<Event> EventHistory { get; set; } = [];
    public int HealthRecordId { get; set; }

    public async Task<ActionResult> OnGet(int healthRecordId) {
        if (healthRecordId == 0) {
            return BadRequest("Ett fel har inträffat");
        }
        EventHistory = [.. (await healthRecordService.GetHealthRecordById(healthRecordId))?.Events];

        return Page();
    }
}
