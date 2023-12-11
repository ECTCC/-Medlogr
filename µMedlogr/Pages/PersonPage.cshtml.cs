using µMedlogr.core.Models;
using µMedlogr.core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics.CodeAnalysis;

namespace µMedlogr.Pages;
#pragma warning disable S3604
public class PersonPageModel(PersonService personService, UserManager<AppUser> userManager) : PageModel {
    #region Properties
    public Person? Me { get; set; }
    public List<string> GenderList { get; set; } = [];
    public List<string> AllergiesList { get; set; } = [];
    public AppUser? MyUser { get; set; }
    public List<Person>? PeopleInCareOf { get; set; } = [];
    #endregion
    #region BindProperties
    [BindProperty]
    public List<string> SelectedAllergies { get; set; } = [];
    [BindProperty]
    public bool IsPerson { get; set; }
    [BindProperty]
    public Person? Person { get; set; }
    [BindProperty]
    public DateOnly SelectedDate { get; set; }
    [BindProperty]
    public List<string> EditListAllergies { get; set; } = [];
    [BindProperty]
    public DateOnly EditBirthDate { get; set; }
    [BindProperty]
    public string EditNickName { get; set; } = "";
    [BindProperty]
    public float? EditedWeight { get; set; }
    #endregion

    public async Task<IActionResult> OnGetAsync() {
        AllergiesList = PersonPage.CreateAllergiesList();
        MyUser = await userManager.GetUserAsync(User);
        if (MyUser is not null) {
            MyUser = await personService.GetAppUserWithRelationsById(MyUser.Id);
            PeopleInCareOf = MyUser?.PeopleInCareOf.ToList() ?? [];
            Me = MyUser?.Me;
        }
        return Page();
    }
    public async Task<IActionResult> OnPostSavePersonAsync() {
        MyUser = await userManager.GetUserAsync(User);
        if (MyUser == null) {
            // Put Error Message in Tempdata
            return RedirectToPage("/PersonPage");
        }
        Person.Allergies = PersonPage.ReturnSameListOrAddStringNoAllergy(SelectedAllergies);
        Person.DateOfBirth = SelectedDate;
        var myperson = await personService.GetAppUsersPersonById(MyUser.Id);
        if (IsPerson) {
            if (myperson == null) {
                var healthrecord = new HealthRecord {
                    Person = Person
                };
                Person.HealthRecord = healthrecord;
                MyUser.Me = Person;
                await personService.UpdateAppUser(MyUser);
            } else {
                MyUser.Me!.Allergies = PersonPage.ReturnSameListOrAddStringNoAllergy(SelectedAllergies);
                MyUser.Me.DateOfBirth = SelectedDate;
                MyUser.Me.NickName = Person.NickName;
                MyUser.Me.WeightInKg = Person.WeightInKg;
                await personService.UpdatePerson(MyUser.Me);
            }
        } else {
            var healthrecord = new HealthRecord {
                Person = Person
            };
            Person.HealthRecord = healthrecord;
            MyUser.PeopleInCareOf.Add(Person);
            await personService.UpdateAppUser(MyUser);
        }
        return RedirectToPage("/PersonPage");
    }
    public async Task<IActionResult> OnPostEditPersonInCareOfAsync(int id) {
        var person = await personService.FindPerson(id);
        var allergies = PersonPage.ReturnSameListOrAddStringNoAllergy(EditListAllergies);
        if (EditNickName is null) {
            return RedirectToPage("/PersonPage");
        }
        person.DateOfBirth = EditBirthDate;
        person.NickName = EditNickName;
        person.Allergies = allergies;
        person.WeightInKg = EditedWeight;
        var success = await personService.UpdatePerson(person);
        if (!success) {
            //Error handeling here or error message.
        }
        return RedirectToPage("/PersonPage");
    }
    public async Task<IActionResult> OnPostDeletePersonAsync(int id) {
        var person = await personService.FindPerson(id);
        if (person is not null) {
            await personService.DeletePerson(person);
        }
        return RedirectToPage("/PersonPage");
    }
}
#pragma warning restore S3604
