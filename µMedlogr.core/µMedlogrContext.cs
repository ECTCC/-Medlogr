using µMedlogr.core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace µMedlogr.core;
public class µMedlogrContext : IdentityDbContext<AppUser> {
    internal DbSet<HealthRecord> HealthRecords { get; set; } = default!;
    internal DbSet<Person> People { get; set; } = default!;
    internal DbSet<SymptomMeasurement> SymptomMeasurements { get; set; } = default!;
    internal DbSet<SymptomType> SymptomTypes { get; set; } = default!;
    internal DbSet<TemperatureData> TemperatureDatas { get; set; } = default!;
    internal DbSet<HealthRecordEntry> HealthRecordsEntrys { get; set; }=default!;
    internal DbSet<AppUser> AppUsers { get; set; }= default!;
    internal DbSet<Drug> Drugs { get; set; } = default!;
    internal DbSet<Event> Events { get; set; } = default!;

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

        builder.Entity<AppUser>()
            .HasOne(x => x.Me);

        builder.Entity<HealthRecord>()
            .HasOne(x => x.Person)
            .WithOne(x => x.HealthRecord)
            .HasForeignKey<HealthRecord>("PersonId");

        InitData(builder);

    }

    private void InitData(ModelBuilder builder) {
        //Skapa object innan
        var kalle = new Person { Id = 1, NickName = "Nisse", WeightInKg = 47 };

        builder.Entity<Person>().HasData(
            kalle
            );
        builder.Entity<HealthRecord>().HasData(
            new HealthRecord{ Id = 1, PersonId = kalle.Id}
            );
    }
}
