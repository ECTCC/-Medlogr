using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Tracing;

namespace µMedlogr.core.Models;
[PrimaryKey(nameof(Id))]
public class HealthRecord : Entity {
    public int PersonId { get; set; }
    public Person Person { get; set; } = default!;
    public virtual ICollection<HealthRecordEntry> Entries { get; set; }
    public virtual ICollection<TemperatureData> Temperatures { get; set; }
    public virtual ICollection<SymptomType> CurrentSymptoms { get; set; }
    public virtual ICollection<Event> Events { get; set; }

    internal HealthRecord() {
        Temperatures = new HashSet<TemperatureData>();
        Entries = new HashSet<HealthRecordEntry>();
        CurrentSymptoms = new HashSet<SymptomType>();
        Events = new HashSet<Event>();
    }

}
