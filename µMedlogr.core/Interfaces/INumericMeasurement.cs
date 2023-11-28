using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace µMedlogr.core.Interfaces;
internal interface INumericMeasurement<T> where T : INumber<T> {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    internal int Id { get; set; }
    internal DateTime TimeOfMeasurement { get; set; }
    internal T Measurement { get; set; }
    internal string? Comments { get; set; }

}
