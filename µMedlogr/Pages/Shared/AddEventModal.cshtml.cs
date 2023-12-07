using µMedlogr.core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using µMedlogr.core.Services;

namespace µMedlogr.Pages.Shared;
public class AddEventModalModel(HealthRecordService healthRecordService) : PageModel {
    private readonly HealthRecordService _healthService = healthRecordService;

    [BindProperty]
    public Event Event { get; set; } = new Event();
    [BindProperty]
    public int HealthRecordId { get; set; }
    [BindProperty]
    public SelectList DrugChoices { get; set; } = default!;
    public IActionResult OnGetPartial(int healthrecordId) {
        var healthrecord = _healthService.Find(healthrecordId);

        //DrugChoices = new SelectList(drugs, nameof(Drug.Id), nameof(Drug.Name));

        HealthRecordId = healthrecordId;
        AddEventModalModel viewdata = new(_healthService) { HealthRecordId = healthrecordId};
        return Partial("Shared/AddEventModal", viewdata);
    }
}
