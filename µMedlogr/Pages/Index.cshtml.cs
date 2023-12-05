using µMedlogr.core;
using µMedlogr.core.Enums;
using µMedlogr.core.Models;
using µMedlogr.core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace µMedlogr.Pages;

public class IndexModel(EntityManager entityManager, µMedlogrContext context) : PageModel
{
    private readonly EntityManager _entityManager = entityManager;
    private readonly µMedlogrContext _context = context;
    
    [Required]
    [BindProperty]
    public string? NewSymptom { get; set; }

    [BindProperty]
    public string? Notes { get; set; }
    [BindProperty(SupportsGet = true)]
    public List<Tuple<int, Severity>> SymptomSeverityList { get; set; } = [];
    [BindProperty]
    public int SymptomId { get; set; }
    public SelectList SymptomChoices { get; set; } = default!;
    [Required]
    [EnumDataType(typeof(Severity))]
    [Range(0, (double)Severity.Maximal, ErrorMessage = "Välj en allvarlighetsgrad")]
    [BindProperty]
    public Severity NewSeverity { get; set; }
    [BindProperty]
    public Person Person { get; set; }



    public async Task OnGetAsync([FromQuery] string json, int helthRekordId)
    {
        if (json is not null)
        {
            var options = new JsonSerializerOptions { WriteIndented = false };
            this.SymptomSeverityList = JsonSerializer.Deserialize<List<Tuple<int, Severity>>>(json) ?? [];
        }
        //Load the SymptomChoices from DB
        var Symptoms = await _context.SymptomTypes.ToListAsync();
        SymptomChoices = new SelectList(Symptoms, nameof(SymptomType.Id), nameof(SymptomType.Name));
        var currentHealthrekord = _context.HealthRecords
            .Where(hr => hr.Id == helthRekordId)
            .Include(hr => hr.Person).FirstOrDefault();
        var currentPerson = currentHealthrekord?.Person;
        Person = currentPerson;
    }

    [ActionName("SaveSymptoms")]
    public async Task<IActionResult> OnPostAsync([FromForm] string json, int helthRekordId)
    {
        if (json is not null)
        {
            var options = new JsonSerializerOptions { WriteIndented = false };
            this.SymptomSeverityList = JsonSerializer.Deserialize<List<Tuple<int, Severity>>>(json) ?? [];
        }
        if (SymptomSeverityList.Count < 1)
        {
            return BadRequest("Inga nya symptom!");
        }

        var measurements = new List<SymptomMeasurement>();
        foreach (var (symptomId, severity) in SymptomSeverityList)
        {
            var newMeasurement = await _entityManager.CreateSymptomMeasurement(symptomId, severity);
            if (newMeasurement != null) {
                measurements.Add(newMeasurement);
            }
        }
        measurements.RemoveAll(item => item == null);
        try
        {
            foreach (var measurement in measurements)
            {
                await _entityManager.SaveNewSymptomMeasurement(measurement);
            }
        }
        catch(Exception ex)
        {
            
        }
        return RedirectToPage("/index", new { json });
    }

    [ActionName("AddSymptom")]
    public async Task<IActionResult> OnPostAddSymptomAsync([FromForm] string injson)
    {
        if (injson is not null)
        {
            var options = new JsonSerializerOptions { WriteIndented = false };
            this.SymptomSeverityList = JsonSerializer.Deserialize<List<Tuple<int, Severity>>>(injson) ?? [];
        }

        if (NewSeverity > 0 && SymptomId > 0)
        {
            SymptomSeverityList.Add(new Tuple<int, Severity>(SymptomId, NewSeverity));
            var options = new JsonSerializerOptions { WriteIndented = false };
            var json = JsonSerializer.Serialize(SymptomSeverityList, options);
            return RedirectToPage("/Index", new { json });
        }
        return BadRequest("Symptom saknas!");
    }
}
