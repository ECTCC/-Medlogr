using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace µMedlogr.core.Models;
[PrimaryKey(nameof(Id))]
public class HealthRecordEntry {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? Notes { get; set; }
    public virtual ICollection<SymptomMeasurement> Measurements { get; set; }

    public HealthRecordEntry() {
        Measurements = new HashSet<SymptomMeasurement>();
    }
}
