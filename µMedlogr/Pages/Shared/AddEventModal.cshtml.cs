using µMedlogr.core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using µMedlogr.core.Services;

namespace µMedlogr.Pages.Shared;
public class AddEventModalModel([FromServices]DrugService drugservice) : PageModel {
    private readonly DrugService _drugService = drugservice;

    [BindProperty]
    public Event Event { get; set; } = new Event();
    [BindProperty]
    public int HealthRecordId { get; set; }
    [BindProperty]
    public SelectList DrugChoices { get; set; } = default!;
    public IActionResult OnGetPartial(int healthrecordId) {
        List<(int id, string name)> drugs = _drugService.GetAll().Select(x=> (x.Id, x.Name)).ToList();

        DrugChoices = new SelectList(drugs, nameof(Drug.Id), nameof(Drug.Name));

        HealthRecordId = healthrecordId;
        AddEventModalModel viewdata = new(_drugService) { HealthRecordId = healthrecordId};
        return Partial("Shared/AddEventModal", viewdata);
    }
}
