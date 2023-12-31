﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace µMedlogr.core.Models;
[PrimaryKey(nameof(Id))]
public class Person : Entity {
    [Required]
    public string? NickName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    [NotMapped]
    public TimeSpan Age { get => new (DateTime.Now.Ticks - DateOfBirth.ToDateTime(TimeOnly.MinValue).Ticks); }
    public float? WeightInKg { get; set; }
    public List<string> Allergies { get; set; } = [];
    public HealthRecord HealthRecord { get; set; } = default!;

    public virtual ICollection<AppUser> CareGivers { get;}

    public Person()
    {
        CareGivers = new HashSet<AppUser>();
    }

}
