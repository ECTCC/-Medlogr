using µMedlogr.core;
using µMedlogr.core.Models;
using µMedlogr.core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace µMedlogr.Pages;
public class LogTemperatureModel : PageModel {
    public readonly UserManager<AppUser> _userManager;
    private readonly EntityManager _entityManager;
    private readonly µMedlogrContext _context;
    public List<TemperatureData> Temperatures { get; set; } = [];
    public string Nickname { get; set; } = string.Empty;
    public int HealthRecordId { get; set; }
    [BindProperty]
    [Range(30, 45, ErrorMessage = "Value for {0} must be between {1} and {2}")]
    public float NewTemperature { get; set; }
    [BindProperty]
    public string Notes { get; set; } = string.Empty;
    public LogTemperatureModel(EntityManager entityManager, µMedlogrContext context, UserManager<AppUser> userManager) {
        _entityManager = entityManager;
        _context = context;
        _userManager = userManager;
    }
    public void OnGet(int healthRecordId) {
        Nickname = _context.HealthRecords
            .Find(healthRecordId)?
            .Person?.NickName ?? "Anonym person";
        Temperatures = _entityManager.GetTemperatureDataByHealthRecordId(healthRecordId).ToList();
        HealthRecordId = healthRecordId;
    }

    public async Task<IActionResult> OnPostAsync(int healthRecordId) {
        if (!ModelState.IsValid) {
            return BadRequest(ModelState);
        }

        _entityManager.SaveTemperatureInHealthRecord(healthRecordId);
        return RedirectToPage("/Index");
    }
}
