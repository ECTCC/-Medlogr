using µMedlogr.core.Models;
using Microsoft.EntityFrameworkCore;

namespace µMedlogr.core;
public class µMedlogrContext : DbContext {

    public µMedlogrContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder) {
        // Add Loading behavior for related entities
    }

    public DbSet<HealthRecord> HealthRecords { get; set; } = default!;
    public DbSet<Person> People { get; set; } = default!;
    public DbSet<SymptomMeasurement> SymptomMeasurements { get; set; } = default!;
    public DbSet<SymptomType> SymptomTypes { get; set; } = default!;
    public DbSet<TemperatureData> TemperatureDatas { get; set; } = default!;

}
