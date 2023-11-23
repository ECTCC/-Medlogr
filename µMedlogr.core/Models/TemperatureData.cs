using µMedlogr.core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace µMedlogr.core.Models;
internal class TemperatureData : INumericMeasurement<float> {
    public int Id { get; set; }
    public DateTime TimeOfMeasurement { get; set; }
    public float Measurement { get; set; }
    public string Comments { get; set; }
}
