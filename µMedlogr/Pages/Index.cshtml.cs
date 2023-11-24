using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using µMedlogr.core.Enums;
using µMedlogr.core.Models;

namespace µMedlogr.Pages;

public class IndexModel : PageModel
{

    [Required, Display(Name = "Symptom")]
    internal string NewSymptom { get; set; }

    internal List<(SymptomType,Severity)> SymptomSeverityList { get; set; }

    /// <summary>
    /// The database choices for symptoms
    /// </summary>
    //internal List<SymptomType> SymptomChoices { get; set; }
    //internal SelectList SymptomChoices { get; set; }
    internal SymptomType NewSymptomType { get; set; }

    [Required, EnumDataType(typeof(µMedlogr.core.Enums.Severity))]
    internal Severity NewSeverity { get; set; }
    //public List<string> Severitys { get; set; }
    public IndexModel()
    {
        SymptomSeverityList = [];

    }
    public async void OnGetAsync(){

        //Load the SymptomChoices from DB
        // SymptomChoices = await _context.SymptomTypes.ToListAsync();


    }
}
