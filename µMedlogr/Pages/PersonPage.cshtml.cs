using µMedlogr.core.Models;
using µMedlogr.core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace µMedlogr.Pages;
public class PersonPageModel(EntityManager entityManager) : PageModel {
    private readonly EntityManager _entityManager = entityManager;
    [BindProperty]
    public Person Person { get; set; } = default!;
    public List<string> GenderList { get; set; } = [];
    public List<string> AllergiesList { get; set; } = [];
    [BindProperty]
    public List<string> SelectedAllergies { get; set; } = [];
    [BindProperty]
    public DateOnly SelectedDate { get; set; }
    public void OnGet() {
        GenderList = PersonPage.CreateGenderList();
        AllergiesList = PersonPage.CreateAllergiesList();

    }
    public async Task<IActionResult> OnPostSavePersonAsync() {
        Person.Allergies = PersonPage.ReturnSameListOrAddStringNoAllergy(SelectedAllergies);
        Person.DateOfBirth = SelectedDate;
        var healthrecord = new HealthRecord();
        healthrecord.Person = Person;
        Person.HealthRecord = healthrecord;
        await _entityManager.SaveNewPerson(Person);
        return RedirectToPage("/PersonPage");
    }
}
