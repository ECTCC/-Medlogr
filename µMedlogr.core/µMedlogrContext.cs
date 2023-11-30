using µMedlogr.core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace µMedlogr.core;
public class µMedlogrContext : IdentityDbContext<AppUser> {

    public µMedlogrContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<AppUser>()
            .HasMany(x => x.PeopleInCareOf)
            .WithMany(x => x.CareGivers)
            .UsingEntity<Dictionary<string, object>>(
                "AppUserPerson",
                j=> j
                .HasOne<AppUser>()
                .WithMany()
                .HasForeignKey("CareGiversId")
                .OnDelete(DeleteBehavior.Restrict)
                );
        builder.Entity<AppUser>().HasOne(x => x.Me);
        builder.Entity<HealthRecord>()
            .HasOne(x => x.Record)
            .WithOne(x => x.HealthRecord)
            .HasForeignKey<Person>(x => x.Id);

    }

    internal virtual DbSet<HealthRecord> HealthRecords { get; set; } = default!;
    internal virtual DbSet<Person> People { get; set; } = default!;
    internal virtual DbSet<SymptomMeasurement> SymptomMeasurements { get; set; } = default!;
    internal virtual DbSet<SymptomType> SymptomTypes { get; set; } = default!;
    internal virtual DbSet<TemperatureData> TemperatureDatas { get; set; } = default!;
    internal virtual DbSet<HealthRecordEntry> HealthRecordsEntrys { get; set; }=default!;
    internal virtual DbSet<AppUser> AppUsers { get; set; }= default!;
}
