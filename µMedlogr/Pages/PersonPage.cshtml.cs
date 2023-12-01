using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using µMedlogr.core;
using µMedlogr.core.Interfaces;
using µMedlogr.core.Models;
using µMedlogr.core.Services;
using Microsoft.AspNetCore.Identity;

namespace µMedlogr.Pages
{
    public class PersonPageModel(EntityManager entityManager,UserManager<AppUser> userManager) : PageModel
    {
        private readonly EntityManager _entityManager = entityManager;
        private readonly UserManager<AppUser> _userManager =userManager;

        [BindProperty]
        public Person Person { get; set; }
        public List<string> GenderList { get; set; }
        public List<string> AllergiesList { get; set; }
        [BindProperty]
        public List<string> SelectedAllergies { get; set; }
        [BindProperty]
        public DateOnly SelectedDate { get; set; }
        public AppUser MyUser { get; set; }
        public async void OnGet()
        {
            AllergiesList = core.Services.PersonPage.CreateAllergiesList();
        }
        public async Task<IActionResult> OnPostSavePersonAsync()
        {
            MyUser = await _userManager.GetUserAsync(User);MyUser = await _userManager.GetUserAsync(User);
            Person.Allergies = core.Services.PersonPage.ReturnSameListOrAddStringNoAllergy(SelectedAllergies);
            Person.DateOfBirth=SelectedDate;
            var healthrecord=new core.Models.HealthRecord();
            healthrecord.Person = Person;
            Person.HealthRecord=healthrecord;
            MyUser.PeopleInCareOf.Add(Person);
            await _entityManager.SaveNewPerson(Person);
            return RedirectToPage("/PersonPage");
        }
    }
}
