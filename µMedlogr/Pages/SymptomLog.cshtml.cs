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
using System.Diagnostics.Metrics;

namespace µMedlogr.Pages;

public class SymptomLogModel(EntityManager entityManager, µMedlogrContext context) : PageModel
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
    public SelectList SymptomChoices { get; set; }
 
    [Required]
    [EnumDataType(typeof(µMedlogr.core.Enums.Severity))]
    [Range(0, (double)µMedlogr.core.Enums.Severity.Maximal, ErrorMessage = "Välj en allvarlighetsgrad")]
    [BindProperty]
    public Severity NewSeverity { get; set; }
    [BindProperty]
    public Person Person { get; set; }
    //[BindProperty]
    //public HealthRecord HealthRecord { get; set; }

    public async Task OnGetAsync([FromQuery] string json, int healthRecordId = 2)
    {
        if (json is not null)
        {
            var options = new JsonSerializerOptions { WriteIndented = false };
            this.SymptomSeverityList = JsonSerializer.Deserialize<List<Tuple<int, Severity>>>(json) ?? [];
        }
        //Load the SymptomChoices from DB
        var Symptoms = await _context.SymptomTypes.ToListAsync();
        SymptomChoices = new SelectList(Symptoms, nameof(SymptomType.Id), nameof(SymptomType.Name));

        var currentHealthRecord = _context.HealthRecords
            .Where(hr => hr.Id == healthRecordId)
            .FirstOrDefault();
        

        var currentPersonId = await _context.HealthRecords
           .Where(hr => hr.Id == healthRecordId)
           .Select(hr => hr.PersonId)
           .FirstOrDefaultAsync();

        var currentPerson = await _context.People
            .Where(person => person.Id == currentPersonId)
            .FirstOrDefaultAsync();
        Person = currentPerson;

    }

    [ActionName("SaveSymptoms")]
    public async Task<IActionResult> OnPostAsync([FromForm] string json, int healthRecordId)
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

        var healthRecordEntry = new HealthRecordEntry
        {
            Notes = Notes,
            TimeSymptomWasChecked = DateTime.Now,

        };
        
        foreach(var (symptomId,severity) in SymptomSeverityList)
        {
            var measurment = await _entityManager.CreateSymptomMeasurement(symptomId, severity);
            healthRecordEntry.Measurements.Add(measurment);
        }
       
  Person.HealthRecord.Entries.Add(healthRecordEntry);
        try
        {
            await _entityManager.SaveNewHealthRecordEntry(healthRecordEntry);  
        }
        catch (Exception ex)
        {

        }
        return RedirectToPage("/SymptomLog");
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
            return RedirectToPage("/SymptomLog", new { json });
        }
        return BadRequest("Symptom saknas!");
    }
}
