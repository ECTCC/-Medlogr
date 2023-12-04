using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using µMedlogr.core;
using µMedlogr.core.Interfaces;
using µMedlogr.core.Models;
using µMedlogr.core.Services;
using Microsoft.AspNetCore.Identity;

namespace µMedlogr.Pages
{
    public class PersonPageModel(EntityManager entityManager, UserManager<AppUser> userManager) : PageModel
    {
        private readonly EntityManager _entityManager = entityManager;
        private readonly UserManager<AppUser> _userManager = userManager;

        [BindProperty]
        public Person Person { get; set; }
        public List<string> GenderList { get; set; }
        public List<string> AllergiesList { get; set; }
        [BindProperty]
        public List<string> SelectedAllergies { get; set; }
        [BindProperty]
        public bool IsPerson { get; set; }
        [BindProperty]
        public DateOnly SelectedDate { get; set; }
        public AppUser MyUser { get; set; }
        public List<Person> PeopleInCareOf { get; set; }
        public Person Me { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            AllergiesList = core.Services.PersonPage.CreateAllergiesList();
            MyUser = await _userManager.GetUserAsync(User);
            if(MyUser is not null)
            {
                PeopleInCareOf = await _entityManager.GetJunctionData(MyUser);
                Me = await _entityManager.GetMeFromUser(MyUser);
            }
            return Page();
        }
        public async Task<IActionResult> OnPostSavePersonAsync()
        {
            MyUser = await _userManager.GetUserAsync(User);
            Person.Allergies = core.Services.PersonPage.ReturnSameListOrAddStringNoAllergy(SelectedAllergies);
            Person.DateOfBirth = SelectedDate;
            var healthrecord = new core.Models.HealthRecord();
            healthrecord.Person = Person;
            Person.HealthRecord = healthrecord;
            if (MyUser is not null)
            {
                if (IsPerson == true)
                {
                    MyUser.Me = Person;
                }
                else
                {
                    MyUser.PeopleInCareOf.Add(Person);
                }
            }
            await _entityManager.SaveNewPerson(Person);
            return RedirectToPage("/PersonPage");
        }
    }
}
