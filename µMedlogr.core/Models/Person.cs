using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace µMedlogr.core.Models;
[PrimaryKey(nameof(Id))]
internal class Person {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public string? NickName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    [NotMapped]
    public TimeSpan Age { get; }
    public float? WeightInKg { get; set; }
    public List<string> Allergies { get; set; } = [];

}
