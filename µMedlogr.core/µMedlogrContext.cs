using µMedlogr.core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace µMedlogr.core;
public class µMedlogrContext : IdentityDbContext<AppUser> {
    internal DbSet<HealthRecord> HealthRecords { get; set; } = default!;
    internal virtual DbSet<Person> People { get; set; } = default!;
    internal DbSet<SymptomMeasurement> SymptomMeasurements { get; set; } = default!;
    internal DbSet<SymptomType> SymptomTypes { get; set; } = default!;
    internal DbSet<TemperatureData> TemperatureDatas { get; set; } = default!;
    internal DbSet<HealthRecordEntry> HealthRecordsEntrys { get; set; } = default!;
    internal DbSet<AppUser> AppUsers { get; set; } = default!;
    internal DbSet<Drug> Drugs { get; set; } = default!;
    internal DbSet<Event> Events { get; set; } = default!;

    public µMedlogrContext(DbContextOptions options) : base(options) { }
    public µMedlogrContext() { }

    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);

        ConfigurePerson(builder);
        ConfigureHealthRecordEntry(builder);
        ConfigureSymptomMeasurement(builder);
        ConfigureTemperatureData(builder);
        ConfigureHealthRecord(builder);
        ConfigureEvent(builder);
        ConfigureUser(builder);
        builder.Seed();
    }

    private void ConfigurePerson(ModelBuilder builder) {
        builder.Entity<Person>().Navigation(x => x.HealthRecord).AutoInclude();
    }

    private static void ConfigureSymptomMeasurement(ModelBuilder builder) {
        builder.Entity<SymptomMeasurement>()
            .HasOne<HealthRecordEntry>()
            .WithMany(x => x.Measurements)
            .OnDelete(DeleteBehavior.Cascade);
    }
    private static void ConfigureHealthRecordEntry(ModelBuilder builder) {
        builder.Entity<HealthRecordEntry>()
            .HasOne<HealthRecord>()
            .WithMany(x => x.Entries)
            .OnDelete(DeleteBehavior.Cascade);
    }
    private static void ConfigureTemperatureData(ModelBuilder builder) {
        builder.Entity<TemperatureData>()
            .HasOne<HealthRecord>()
            .WithMany(x => x.Temperatures)
            .OnDelete(DeleteBehavior.Cascade);
    }
    private static void ConfigureHealthRecord(ModelBuilder builder) {
        builder.Entity<HealthRecord>()
            .HasOne(x => x.Person)
            .WithOne(x => x.HealthRecord)
            .HasForeignKey<HealthRecord>("PersonId");
    builder.Entity<HealthRecord>()
            .Navigation(x => x.Person).AutoInclude();
        builder.Entity<HealthRecord>()
            .Navigation(x => x.Entries).AutoInclude();
        builder.Entity<HealthRecord>()
            .Navigation(x => x.Events).AutoInclude();
        builder.Entity<HealthRecord>()
            .Navigation(x => x.Temperatures).AutoInclude();
    }

    private static void ConfigureEvent(ModelBuilder builder) {
        builder.Entity<Event>()
            .HasMany(x => x.AdministeredMedicines)
            .WithOne()
            .OnDelete(DeleteBehavior.SetNull);

        builder.Entity<Event>()
            .HasOne<HealthRecord>()
            .WithMany(x => x.Events)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private static void ConfigureUser(ModelBuilder builder) {
        builder.Entity<AppUser>()
            .HasMany(x => x.PeopleInCareOf)
            .WithMany(x => x.CareGivers)
            .UsingEntity<Dictionary<string, object>>(
                "AppUserPerson",
                j => j
                .HasOne<AppUser>()
                .WithMany()
                .HasForeignKey("CareGiversId")
                .OnDelete(DeleteBehavior.Restrict)
                );

        builder.Entity<AppUser>()
            .HasOne(x => x.Me);
    }
}
