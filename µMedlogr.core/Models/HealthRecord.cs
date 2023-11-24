using System.ComponentModel.DataAnnotations;
using µMedlogr.core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace µMedlogr.core.Models;
[PrimaryKey(nameof(Id))]
public class HealthRecord {
    internal int Id { get; set; }
    public required Person Record { get; set; }
    internal ICollection<INumericMeasurement<float>> Temperatures { get; set; }
    public ICollection<SymptomMeasurement> SymtomLog { get; set; }
    public ICollection<SymptomType> CurrentSymptoms { get; set; }

    public HealthRecord() {
        Temperatures = new HashSet<INumericMeasurement<float>>();
        SymtomLog = new HashSet<SymptomMeasurement>();
        CurrentSymptoms = new HashSet<SymptomType>();
    }

}
