using System.ComponentModel.DataAnnotations;
using µMedlogr.core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace µMedlogr.core.Models;
[PrimaryKey(nameof(Id))]
internal class HealthRecord {
    public int Id { get; set; }
    public Person? Record { get; set; }
    public ICollection<TemperatureData> Temperatures { get; set; }
    public ICollection<SymptomMeasurement> SymtomLog { get; set; }
    public ICollection<SymptomType> CurrentSymptoms { get; set; }

    internal HealthRecord() {
        Temperatures = new HashSet<TemperatureData>();
        SymtomLog = new HashSet<SymptomMeasurement>();
        CurrentSymptoms = new HashSet<SymptomType>();
    }

}
