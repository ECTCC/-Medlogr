﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace µMedlogr.core.Models;
[PrimaryKey(nameof(Id))]
internal class HealthRecord {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public Person? Record { get; set; }
    public virtual ICollection<HealthRecordEntry> Entries { get; set; }
    public virtual ICollection<TemperatureData> Temperatures { get; set; }
    public virtual ICollection<SymptomType> CurrentSymptoms { get; set; }

    internal HealthRecord() {
        Temperatures = new HashSet<TemperatureData>();
        Entries = new HashSet<HealthRecordEntry>();
        CurrentSymptoms = new HashSet<SymptomType>();
    }

}
