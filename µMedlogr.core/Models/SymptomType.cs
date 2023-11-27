using µMedlogr.core.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace µMedlogr.core.Models;
[PrimaryKey(nameof(Id))]
internal class SymptomType {
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }

    public ICollection<HealthRecord> Records { get; set; }

    /// <summary>
    /// The duration for which a symptom will be reported 
    /// as active before a new assessment is needed
    /// </summary>
    //public float MeasureInterval { get; set; }

    public SymptomType() { 
        Records = new HashSet<HealthRecord>();
    }
}
