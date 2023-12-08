using µMedlogr.core.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using µMedlogr.core.Exceptions;

namespace µMedlogr.core.Models;
[PrimaryKey(nameof(Id))]
public class TemperatureData : Entity, INumericMeasurement<float> {
    public DateTime TimeOfMeasurement { get; set; }
    private float _measurement;
    public float Measurement {
        get => _measurement;
        set {
            if (value < 35 || value > 45) {
                throw new TemperatureOutOfRangeException("Temperature must be between 35 and 45 degrees");
            }
            _measurement = value;
        }
    }
    public string? Comments { get; set; }
}
