using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace µMedlogr.core.Models;
[PrimaryKey(nameof(Id))]
public class SymptomType : Entity {
    [Required]
    public string? Name { get; set; }

    public virtual ICollection<HealthRecord> Records { get; set; }

    /// <summary>
    /// The duration for which a symptom will be reported 
    /// as active before a new assessment is needed
    /// </summary>
    public TimeSpan MeasureInterval { get; set; }

    public SymptomType() {
        Records = new HashSet<HealthRecord>();
    }
}
