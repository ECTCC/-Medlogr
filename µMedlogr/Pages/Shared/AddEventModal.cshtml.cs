using µMedlogr.core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace µMedlogr.Pages.Shared;
public class AddEventModalModel() : PageModel {
    [BindProperty]
    public Event Event { get; set; } = new Event();
    [BindProperty]
    public int HealthrecordId { get; set; }
    public IActionResult OnGetPartial(int healthrecordId) {
        HealthrecordId = healthrecordId;
        AddEventModalModel viewdata = new() { HealthrecordId = healthrecordId};
        return Partial("Shared/AddEventModal", viewdata);
    }
}
