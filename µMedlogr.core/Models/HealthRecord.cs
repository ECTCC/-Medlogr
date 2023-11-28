using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace µMedlogr.core.Models;
[PrimaryKey(nameof(Id))]
internal class HealthRecord {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public Person? Record { get; set; }
    public virtual ICollection<TemperatureData> Temperatures { get; set; }
    public virtual ICollection<SymptomMeasurement> SymtomLog { get; set; }
    public virtual ICollection<SymptomType> CurrentSymptoms { get; set; }

    internal HealthRecord() {
        Temperatures = new HashSet<TemperatureData>();
        SymtomLog = new HashSet<SymptomMeasurement>();
        CurrentSymptoms = new HashSet<SymptomType>();
    }

}
