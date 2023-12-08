using µMedlogr.core;
using µMedlogr.core.Enums;
using µMedlogr.core.Models;
using µMedlogr.core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace µMedlogr.Pages;

public class SymptomLogModel : PageModel
{
    public readonly UserManager<AppUser> _userManager;
    private readonly EntityManager _entityManager;
    private readonly µMedlogrContext _context;

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
    public Person? Person { get; set; }
    [BindProperty]
    public int HealthRecordId { get; set; }
    public AppUser? MyUser { get; set; }
    //[BindProperty] 
    //public HealthRecord CurrentHealthRecord { get; set; }


    [BindProperty]
    public List<HealthRecordEntry> CurrentHealthRecordEntries { get; set; }

    public SymptomLogModel(EntityManager entityManager, µMedlogrContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _entityManager = entityManager;
        _userManager = userManager;

    }

    public async Task OnGetAsync([FromQuery] string json, int healthRecordId)
    {
        if (json is not null)
        {
            var options = new JsonSerializerOptions { WriteIndented = false };
            this.SymptomSeverityList = JsonSerializer.Deserialize<List<Tuple<int, Severity>>>(json) ?? [];
        }
        var Symptoms = await _context.SymptomTypes.ToListAsync();
        SymptomChoices = new SelectList(Symptoms, nameof(SymptomType.Id), nameof(SymptomType.Name));

        HealthRecordId = healthRecordId;

            MyUser = await _userManager.GetUserAsync(User);
            Person = _entityManager.GetUserPerson(MyUser.Id);
            //CurrentHealthRecord = _entityManager.GetHealthRecordById(MyUser.Id);

            CurrentHealthRecordEntries = await _entityManager.GetHealthRecordEntriesByHealthRekordId(Person.Id);
    

    }

    [ActionName("SaveSymptoms")]
    public async Task<IActionResult> OnPostAsync([FromForm] string json, int healthRecordId)
    {
        var currentHealthRecord = _context.HealthRecords
            .Where(hr => hr.Id == HealthRecordId)
            .FirstOrDefault();
       
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

        if (currentHealthRecord != null) { 
        foreach (var (symptomId, severity) in SymptomSeverityList)
        {
            var measurment = await _entityManager.CreateSymptomMeasurement(symptomId, severity);
            healthRecordEntry.Measurements.Add(measurment);
        }
        
            currentHealthRecord.Entries.Add(healthRecordEntry);
        }
        else
        {
            return BadRequest("Inga HelthRekordId!");
        }

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

    public async Task<string> GetSymptomName(int symptomId)
    {
        var symptom = await _context.SymptomTypes
            .Where(x => x.Id == symptomId)
            .FirstOrDefaultAsync();

        return symptom == null ? "Symptom unknown" : symptom.Name;

    }
}
