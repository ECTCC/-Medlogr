using System.ComponentModel.DataAnnotations;

namespace µMedlogr.core.Models;
public class Person {
    [Key]
    public int Id { get; set; }
    public required string NickName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public float? WeightInKg { get; set; }
    public List<string> Allergies { get; set; } = [];

}
