﻿using µMedlogr.core.Models;
using Microsoft.EntityFrameworkCore;

namespace µMedlogr.core;
public class µMedlogrContext : DbContext {

    public µMedlogrContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<AppUser>().HasMany(x=>x.PeopleInCareOf).WithMany(x=>x.CareGivers);
        builder.Entity<AppUser>().HasOne(x => x.Me);
        builder.Entity<Person>().HasOne(x => x.HealthRecord);
        builder.Entity<HealthRecord>().HasOne(x => x.Record);
        
    }

    internal DbSet<HealthRecord> HealthRecords { get; set; } = default!;
    internal DbSet<Person> People { get; set; } = default!;
    internal DbSet<SymptomMeasurement> SymptomMeasurements { get; set; } = default!;
    internal DbSet<SymptomType> SymptomTypes { get; set; } = default!;
    internal DbSet<TemperatureData> TemperatureDatas { get; set; } = default!;
    internal DbSet<HealthRecordEntry> HealthRecordsEntrys { get; set; }=default!;
    internal DbSet<AppUser> AppUsers { get; set; }= default!;
}
