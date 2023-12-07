using µMedlogr.core.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace µMedlogr.core.Models;
[PrimaryKey(nameof(Id))]
public class TemperatureData : Entity, INumericMeasurement<float> {
    public DateTime TimeOfMeasurement { get; set; }
    public float Measurement { get; set; }
    public string? Comments { get; set; }
}
