using µMedlogr.core.Enums;

namespace µMedlogr.core.Models;
public class SymptomType {
    public int Id { get; set; }
    public required string Name { get; set; }

    /// <summary>
    /// The duration for which a symptom will be reported 
    /// as active before a new assessment is needed
    /// </summary>
    //public float MeasureInterval { get; set; }
    

}
