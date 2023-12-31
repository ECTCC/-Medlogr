﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace µMedlogr.core.Models;
[PrimaryKey(nameof(Id))]
public class HealthRecordEntry : Entity {
    public string? Notes { get; set; }
    public DateTime TimeSymptomWasChecked { get; set; }
  
    public virtual ICollection<SymptomMeasurement> Measurements { get; set; }

    public HealthRecordEntry() {
        Measurements = new HashSet<SymptomMeasurement>();
    }
}
