using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using µMedlogr.core;
using µMedlogr.core.Interfaces;
using µMedlogr.core.Models;
using µMedlogr.core.Services;

namespace µMedlogr.Pages
{
    public class PersonPageModel(EntityManager entityManager) : PageModel
    {
        private readonly EntityManager _entityManager = entityManager;
        [BindProperty]
        public Person Person { get; set; }
        public List<string> GenderList { get; set; }
        public List<string> AllergiesList { get; set; }
        [BindProperty]
        public List<string> SelectedAllergies { get; set; }
        [BindProperty]
        public DateOnly SelectedDate { get; set; }
        public void OnGet()
        {
            GenderList = core.Services.PersonPage.CreateGenderList();
            AllergiesList = core.Services.PersonPage.CreateAllergiesList();

        }
        public async Task<IActionResult> OnPostSavePersonAsync()
        {
            Person.Allergies = core.Services.PersonPage.ReturnSameListOrAddStringNoAllergy(SelectedAllergies);
            Person.DateOfBirth=SelectedDate;
            var healthrecord=new core.Models.HealthRecord();
            healthrecord.Person = Person;
            Person.HealthRecord=healthrecord;
            //Person.Id = 54;
            await _entityManager.SaveNewPerson(Person);
            var a = 0;
            return RedirectToPage("/PersonPage");
        }
    }
}
