using µMedlogr.core.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace µMedlogr.core.Models;
[PrimaryKey(nameof(Id))]
public class SymptomMeasurement : Entity {
    [Required]
    public SymptomType? Symptom { get; set; }
    public Severity SubjectiveSeverity { get; set; }
    
}
