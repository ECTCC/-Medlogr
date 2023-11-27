using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace µMedlogr.core.Models;
[PrimaryKey(nameof(Id))]
internal class Person {
    public int Id { get; set; }
    [Required]
    public string? NickName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    [NotMapped]
    public TimeSpan Age { get; }
    public float? WeightInKg { get; set; }
    public List<string> Allergies { get; set; } = [];

}
