using µMedlogr.core.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace µMedlogr.core.Models;
[PrimaryKey(nameof(Id))]
internal class SymptomMeasurement {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public SymptomType? Symptom { get; set; }
    public Severity SubjectiveSeverity { get; set; }
    public DateTime TimeSymptomWasChecked { get; set; }
}
