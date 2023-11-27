using µMedlogr.core.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace µMedlogr.core.Models;
[PrimaryKey(nameof(Id))]
internal class SymptomMeasurement {
    public int Id { get; set; }
    [Required]
    public SymptomType? Symptom { get; set; }
    public DateTime TimeSymptomWasChecked { get; set; }
    public Severity SubjectiveSeverity { get; set; }
}
