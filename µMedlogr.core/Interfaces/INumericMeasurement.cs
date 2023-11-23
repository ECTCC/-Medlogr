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
    public int Id { get; set; }
    public DateTime TimeOfMeasurement { get; set; }
    public T Measurement { get; set; }
    public string? Comments { get; set; }

}
