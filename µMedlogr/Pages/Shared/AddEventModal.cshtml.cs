using µMedlogr.core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using µMedlogr.core.Services;
using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;

namespace µMedlogr.Pages.Shared;
public class AddEventModalModel : PageModel {
    private readonly DrugService _drugService;
    public AddEventModalModel(DrugService drugservice) {
        _drugService = drugservice;
        var _drugmap = _drugService.GetAll().Result.ToDictionary(x => x.Id, x => x.Name);
        DrugChoices = new SelectList(_drugmap.OrderBy(x => x.Value), "Key", "Value", 0);
        // How to make by dictionary
        //var drugmap = _drugService.GetAll().ToDictionary(x => x.Id, x => x.Name);
    }
    [BindProperty]
    public Event? Event { get; set; }
    [BindProperty]
    public int HealthRecordId { get; set; }
    [BindProperty]
    public SelectList DrugChoices { get; set; }
    [BindProperty]
    public int[] SelectedDrugs {  get; set; }
    public IActionResult OnGet(int healthrecordId) {
        //List<(int id, string name)> drugs = _drugService.GetAll().Select(x => (x.Id, x.Name)).ToList();

        //DrugChoices = new SelectList(drugs, nameof(Drug.Id), nameof(Drug.Name));

        //HealthRecordId = healthrecordId;
        //AddEventModalModel viewdata = new(_drugService) { HealthRecordId = healthrecordId };
        return Partial("Shared/AddEventModal", this);
    }
}
internal class DrugModel() {
    public int Id { get; set; }
    public string? Name { get; set; }
}
