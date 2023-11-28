using µMedlogr.core.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace µMedlogr.core.Models;
internal class TemperatureData : INumericMeasurement<float> {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int Id { get; set; }
    public DateTime TimeOfMeasurement { get; set; }
    public float Measurement { get; set; }
    public string? Comments { get; set; }
}
