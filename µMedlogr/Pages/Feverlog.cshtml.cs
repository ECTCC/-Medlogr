using µMedlogr.core;
using µMedlogr.core.Models;
using µMedlogr.core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using µMedlogr.core.Exceptions;

namespace µMedlogr.Pages;
public class AddTemperatureModel(EntityManager entityManager, µMedlogrContext context, UserManager<AppUser> userManager) : PageModel {

    public List<TemperatureData> Temperatures { get; set; }
    public string Nickname { get; set; }
    public int HealthRecordId { get; set; }
    [BindProperty]
    [Range(35, 45, ErrorMessage = "Value for {0} must be between {1} and {2}")]
    public float NewTemperature { get; set; }
    [BindProperty]
    public string? Notes { get; set; }

    public void OnGet(int healthRecordId) {
        Nickname = context.HealthRecords
            .Find(healthRecordId)?
            .Person?.NickName ?? "Anonym person";
        Temperatures = entityManager.GetTemperatureDataByHealthRecordId(healthRecordId).ToList();
        HealthRecordId = healthRecordId;
    }

    public IActionResult OnPost(int healthRecordId) {
        if (!ModelState.IsValid) {
            TempData["Error"] = "Modal";
            TempData["Message"] = "Kontrollera angivna data";
            return Page();
        }

        try {
            entityManager.AddTemperatureData(healthRecordId, NewTemperature, Notes);
        } catch (TemperatureOutOfRangeException) {
            TempData["Error"] = "Modal";
            TempData["Message"] = "Kunde inte lägga till temperaturmätning";
            return Page();
        }
        return RedirectToPage("/PersonPage");
    }
}
