using µMedlogr.core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace µMedlogr.core.Models;
internal class TemperatureData : INumericMeasurement<float> {
    [Key]
    public int Id { get; set; }
    public DateTime TimeOfMeasurement { get; set; }
    public float Measurement { get; set; }
    public string? Comments { get; set; }
}
