using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using µMedlogr.core;
using µMedlogr.core.Enums;
using µMedlogr.core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace µMedlogr.Pages;

public class IndexModel(µMedlogrContext context) : PageModel
{
    private readonly µMedlogrContext _context = context;
    [Required]
    [BindProperty]
    public string? NewSymptom { get; set; }

    [BindProperty]
    public string? Notes { get; set; }
    [BindProperty]
    public List<(int, Severity)> SymptomSeverityList { get; set; } = [];
    [BindProperty]
    public int SymptomId { get; set; }
    /// <summary>
    /// The database choices for symptoms
    /// </summary>
    //internal List<SymptomType> SymptomChoices { get; set; }
    public SelectList SymptomChoices { get; set; }
    // [BindProperty]
    // public SymptomType NewSymptomType { get; set; }
    [Required]
    [EnumDataType(typeof(µMedlogr.core.Enums.Severity))]
    [Range(0, (double)µMedlogr.core.Enums.Severity.Maximal, ErrorMessage = "Välj en allvarlighetsgrad")]
    [BindProperty]
    public Severity NewSeverity { get; set; }
    public int SymptomIdProperty
    {
        get => (int)(TempData.Peek(nameof(SymptomIdProperty)) ?? 0);
        set => TempData[nameof(SymptomIdProperty)] = value;
    }

    public Severity NewSeverityProperty
    {
        get => (Severity)(TempData.Peek(nameof(NewSeverityProperty)) ?? 0);
        set => TempData[nameof(NewSeverityProperty)] = (int)value;
    }

    public async Task OnGetAsync()
    {
        //Load the SymptomChoices from DB
        var Symptoms = await _context.SymptomTypes.ToListAsync();
        SymptomChoices = new SelectList(Symptoms, nameof(SymptomType.Id), nameof(SymptomType.Name));

    }

    [ActionName("SaveSymptoms")]
    public async Task<IActionResult> OnPostAsync()
    {
        if (SymptomIdProperty > 0)
        {
            TempData.Remove(nameof(SymptomIdProperty));
            TempData.Remove(nameof(NewSeverityProperty));
            return Redirect("/index");
        }
        var test = Notes;
        return BadRequest("Inga nya symptom!");
    }

    [ActionName("AddSymptom")]
    public async Task<IActionResult> OnPostAddSymptomAsync()
    {
        if (NewSeverity > 0 && SymptomId > 0)
        {
            SymptomIdProperty = SymptomId;
            NewSeverityProperty = NewSeverity;
            SymptomSeverityList.Add(new(SymptomId, NewSeverity));
            return RedirectToPage("/Index");

        }
        return BadRequest("Symptom saknas!");
    }
}
