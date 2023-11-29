namespace µMedlogr.core.Models;
internal class HealthRecordEntry {
    public int Id { get; set; }
    public string? Notes { get; set; }
    public virtual ICollection<SymptomMeasurement> Measurements { get; set; }

    public HealthRecordEntry() {
        Measurements = new HashSet<SymptomMeasurement>();
    }
}
