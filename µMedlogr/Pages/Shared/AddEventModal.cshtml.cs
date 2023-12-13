using µMedlogr.core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using µMedlogr.core.Services;
using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;
using µMedlogr.core.Interfaces;

namespace µMedlogr.Pages.Shared;
public class AddEventModalModel([FromServices] IDrugService drugService) : PageModel {
    [BindProperty]
    public Event? Event { get; set; }
    [BindProperty]
    public int HealthRecordId { get; set; }
    [BindProperty]
    public SelectList DrugChoices { get; set; }
    [BindProperty]
    public int[] SelectedDrugs { get; set; } = [];

    public async Task<IActionResult> OnGet() {
        var _drugmap = (await drugService.GetAllDrugs()).ToDictionary(x => x.Id, x => x.Name);
        DrugChoices = new SelectList(_drugmap.OrderBy(x => x.Value), "Key", "Value", 0);
        return Page();
    }
}
internal class DrugModel() {
    public int Id { get; set; }
    public string? Name { get; set; }
}
