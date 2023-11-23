using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace µMedlogr.core.Interfaces;
internal interface INumericMeasurement<T> where T : INumber<T> {
    [Key]
    internal int Id { get; set; }
    internal DateTime TimeOfMeasurement { get; set; }
    internal T Measurement { get; set; }
    internal string? Comments { get; set; }

}
