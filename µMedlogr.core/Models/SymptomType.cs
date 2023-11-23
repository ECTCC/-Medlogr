using µMedlogr.core.Enums;

namespace µMedlogr.core.Models;
internal class SymptomType {
    public int Id { get; set; }
    public required string Name { get; set; }
    public Severity SubjectiveSeverity { get; set; }

}
