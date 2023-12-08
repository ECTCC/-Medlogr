using µMedlogr.core.Models;
using µMedlogr.core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace µMedlogr.Pages;
public class PersonPageModel(EntityManager entityManager, UserManager<AppUser> userManager) : PageModel {
    private readonly EntityManager _entityManager = entityManager;
    private readonly UserManager<AppUser> _userManager = userManager;

    public Person Me { get; set; }
    public List<string> GenderList { get; set; } = [];
    public List<string> AllergiesList { get; set; } = [];
    public AppUser? MyUser { get; set; }
    public List<Person> PeopleInCareOf { get; set; }

    [BindProperty]
    public List<string> SelectedAllergies { get; set; } = [];
    [BindProperty]
    public bool IsPerson { get; set; }
    [BindProperty]
    public Person Person { get; set; } = new();
    [BindProperty]
    public DateOnly SelectedDate { get; set; }
    [BindProperty]
    public List<string> EditListAllergies { get; set; }
    [BindProperty]
    public DateOnly EditBirthDate { get; set; }
    [BindProperty]
    public string EditNickName { get; set; }
    [BindProperty]
    public float? EditedWeight { get; set; }

    public async Task<IActionResult> OnGetAsync() {
        AllergiesList = PersonPage.CreateAllergiesList();
        MyUser = await _userManager.GetUserAsync(User);
        if(MyUser is not null)
            {
                PeopleInCareOf = await _entityManager.GetJunctionData(MyUser);
                Me = await _entityManager.GetMeFromUser(MyUser);
            }
        return Page();
    }
    public async Task<IActionResult> OnPostSavePersonAsync() {
        MyUser = await _userManager.GetUserAsync(User);
        if (MyUser == null) {
            // Put Error Message in Tempdata
            return RedirectToPage("/PersonPage");
        }
        Person.Allergies = PersonPage.ReturnSameListOrAddStringNoAllergy(SelectedAllergies);
        Person.DateOfBirth = SelectedDate;
        var myperson = _entityManager.GetUserPerson(MyUser.Id);
        if (IsPerson) {
            if (myperson == null) {
                var healthrecord = new HealthRecord {
                    Person = Person
                };
                Person.HealthRecord = healthrecord;
                MyUser.Me = Person;
                await _entityManager.UpdateEntity<AppUser>(MyUser);
            } else {
                MyUser.Me!.Allergies = PersonPage.ReturnSameListOrAddStringNoAllergy(SelectedAllergies);
                MyUser.Me.DateOfBirth = SelectedDate;
                MyUser.Me.NickName = Person.NickName;
                MyUser.Me.WeightInKg = Person.WeightInKg;
                await _entityManager.UpdateEntity<AppUser>(MyUser);
            }
        } else {
            var healthrecord = new HealthRecord {
                Person = Person
            };
            Person.HealthRecord = healthrecord;
            MyUser.PeopleInCareOf.Add(Person);
            await _entityManager.UpdateEntity<AppUser>(MyUser);
        }
      return RedirectToPage("/PersonPage");
    }
    public async Task<IActionResult> OnPostEditPersonInCareOfAsync(int id)
    {
        var person = await _entityManager.GetOnePerson(id);
        var allergies = PersonPage.ReturnSameListOrAddStringNoAllergy(EditListAllergies);
        if(EditNickName is null)
        {
            return RedirectToPage("/PersonPage");
        }
        var success = await _entityManager.EditOnePerson(person,EditNickName,EditBirthDate,EditedWeight,allergies);
        if (success == false)
        {
            //Error handeling here or error message.
        }
        return RedirectToPage("/PersonPage");
    }
    public async Task<IActionResult>OnPostDeletePersonAsync(int id)
    {
        var person = await _entityManager.GetOnePerson(id);
        var success = await _entityManager.DeleteOnePerson(person);
        if(success == false)
        {
            //Error handeling here. 
        }
        return RedirectToPage("/PersonPage");
    }
}
