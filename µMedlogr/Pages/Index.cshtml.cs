using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using µMedlogr.core;
using µMedlogr.core.Enums;
using µMedlogr.core.Models;

namespace µMedlogr.Pages;

public class IndexModel : PageModel
{
    private readonly µMedlogrContext _context;
    [Required]
    [BindProperty]
    public string NewSymptom { get; set; }

    [BindProperty]
    public string Notes { get; set; }
    internal List<(SymptomType,Severity,string)> SymptomSeverityList { get; set; }

    /// <summary>
    /// The database choices for symptoms
    /// </summary>
    //internal List<SymptomType> SymptomChoices { get; set; }
    //internal SelectList SymptomChoices { get; set; }
 // [BindProperty]
   // public SymptomType NewSymptomType { get; set; }

    [Required, EnumDataType(typeof(µMedlogr.core.Enums.Severity))]
    [BindProperty]
    public Severity NewSeverity { get; set; }
    //public List<string> Severitys { get; set; }
    public IndexModel(µMedlogrContext context)
    {
        _context = context;
        SymptomSeverityList = [];

    }
    public async void OnGetAsync(){

        //Load the SymptomChoices from DB
        // SymptomChoices = await _context.SymptomTypes.ToListAsync();

    }
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var test = Notes;

        return Page();
    }
}
