using µMedlogr.core.Enums;
using µMedlogr.core.Interfaces;
using µMedlogr.core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace µMedlogr.Pages;

public class SymptomLogModel(IHealthRecordService healthRecordService, ISymptomService symptomService) : PageModel
{
    public Dictionary<int, string> SymptomChoices { get; set; }
  
    [BindProperty]
    public string? Notes { get; set; }
    [BindProperty(SupportsGet = true)]
    public List<Tuple<int, Severity>> SymptomSeverityList { get; set; } = [];
    [BindProperty]
    public int SymptomId { get; set; }
    [Required]
    [EnumDataType(typeof(Severity))]
    [Range(0, (double)Severity.Maximal, ErrorMessage = "Välj en allvarlighetsgrad")]
    [BindProperty]
    public Severity NewSeverity { get; set; }
    [BindProperty]
    public Person? Person { get; set; }
    [BindProperty]
    public int HealthRecordId { get; set; }
    [BindProperty]
    public List<HealthRecordEntry> CurrentHealthRecordEntries { get; set; }

    public async Task OnGetAsync([FromQuery] string json, int healthRecordId)
    {
        if (json is not null)
        {
            var options = new JsonSerializerOptions { WriteIndented = false };
            this.SymptomSeverityList = JsonSerializer.Deserialize<List<Tuple<int, Severity>>>(json, options) ?? [];
        }
        var symptoms = await symptomService.GetAllSymptoms();
        SymptomChoices = symptoms.ToDictionary(s => s.Id, s => s.Name);
        HealthRecordId = healthRecordId;
      
        var healthRecord = await healthRecordService.GetHealthRecordById(healthRecordId);
        if (healthRecord is not null)
        {
            Person = healthRecord.Person;
            CurrentHealthRecordEntries = await healthRecordService.GetHealthRecordEntriesByHealthRekordId(healthRecordId);
        }
    }

    [ActionName("SaveSymptoms")]
    public async Task<IActionResult> OnPostAsync([FromForm] string json, int healthRecordId) 
    {
        var options = new JsonSerializerOptions { WriteIndented = false };
        this.SymptomSeverityList = JsonSerializer.Deserialize<List<Tuple<int, Severity>>>(json, options) ?? [];

        var currentHealthRecord = await healthRecordService.GetHealthRecordById(healthRecordId);


        if (SymptomSeverityList.Count < 1 || json is null || currentHealthRecord is null) 
        {
            return BadRequest("Något gick fel!");
        }

        var healthRecordEntry = new HealthRecordEntry
        {
            Notes = Notes,
            TimeSymptomWasChecked = DateTime.Now,
        };

        foreach (var (symptomId, severity) in SymptomSeverityList)
        {
            var symptom = await symptomService.FindSymptom(symptomId);
            if (symptom is not null)
            {
                var newMeasurement = new SymptomMeasurement()
                {
                    SubjectiveSeverity = severity,
                    Symptom = symptom
                };
                healthRecordEntry.Measurements.Add(newMeasurement);
            }
        }
        currentHealthRecord.Entries.Add(healthRecordEntry);

        await healthRecordService.AddSymptomMeasurementToHealthRecord(currentHealthRecord, healthRecordEntry);
        return RedirectToPage("/SymptomLog", new { healthRecordId });
    }

    [ActionName("AddSymptom")]
    public async Task<IActionResult> OnPostAddSymptomAsync([FromForm] string injson)
    {
        if (NewSeverity < 0 || SymptomId < 0 || injson is null)
        {
            return BadRequest("Symptom saknas!");
        }
        var jsonOptions = new JsonSerializerOptions { WriteIndented = false };
        this.SymptomSeverityList = JsonSerializer.Deserialize<List<Tuple<int, Severity>>>(injson) ?? [];
        SymptomSeverityList.Add(new Tuple<int, Severity>(SymptomId, NewSeverity));
        var json = JsonSerializer.Serialize(SymptomSeverityList, jsonOptions);
        var healthRecordId = HealthRecordId;
        return RedirectToPage("/SymptomLog", new { json, healthRecordId });
    }
}
