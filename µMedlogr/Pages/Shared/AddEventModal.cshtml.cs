using µMedlogr.core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using µMedlogr.core.Services;
using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;

namespace µMedlogr.Pages.Shared;
public class AddEventModalModel : PageModel {
    [BindProperty]
    public Event? Event { get; set; }
    [BindProperty]
    public int HealthRecordId { get; set; }
    [BindProperty]
    public SelectList DrugChoices { get; set; }
    [BindProperty]
    public int[] SelectedDrugs {  get; set; }
    public AddEventModalModel(DrugService drugService) {
        var _drugmap = drugService.GetAll().Result.ToDictionary(x => x.Id, x => x.Name);
        DrugChoices = new SelectList(_drugmap.OrderBy(x => x.Value), "Key", "Value", 0);
    }
}
internal class DrugModel() {
    public int Id { get; set; }
    public string? Name { get; set; }
}
