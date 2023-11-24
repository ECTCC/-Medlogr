using µMedlogr.core.Enums;

namespace µMedlogr.core.Models;
internal class SymptomMeasurement {
    public int Id { get; set; }
    public required SymptomType Symptom { get; set; }
    public DateTime TimeSymptomWasChecked { get; set; }
    public Severity SubjectiveSeverity { get; set; }
}
