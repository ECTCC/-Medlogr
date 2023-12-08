using µMedlogr.core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using µMedlogr.core.Services;
using System.Diagnostics.CodeAnalysis;

namespace µMedlogr.Pages.Shared;
public class AddEventModalModel : PageModel {
    private readonly DrugService _drugService;
    private class DrugModel {
        public int Id { get; set; } 
        public string Name { get; set; } = string.Empty;
    }
    public AddEventModalModel(DrugService drugservice) {
        _drugService = drugservice;
        var drugs = _drugService.GetAll().Select(x => new DrugModel() { Id=x.Id, Name=x.Name});
        DrugChoices = new SelectList(drugs, nameof(DrugModel.Id), nameof(DrugModel.Name));
    }
    [BindProperty]
    public Event Event { get; set; }
    [BindProperty]
    public int HealthRecordId { get; set; }
    [BindProperty]
    public SelectList DrugChoices { get; set; }
    //public IActionResult OnGetPartial(int healthrecordId) {
    //    List<(int id, string name)> drugs = _drugService.GetAll().Select(x=> (x.Id, x.Name)).ToList();

    //    DrugChoices = new SelectList(drugs, nameof(Drug.Id), nameof(Drug.Name));

    //    HealthRecordId = healthrecordId;
    //    AddEventModalModel viewdata = new(_drugService) { HealthRecordId = healthrecordId};
    //    return Partial("Shared/AddEventModal", viewdata);
    //}
}
