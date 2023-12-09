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
    internal DbSet<HealthRecordEntry> HealthRecordsEntrys { get; set; } = default!;
    internal DbSet<AppUser> AppUsers { get; set; } = default!;
    internal DbSet<Drug> Drugs { get; set; } = default!;
    internal DbSet<Event> Events { get; set; } = default!;

    public µMedlogrContext(DbContextOptions options) : base(options) { }
    public µMedlogrContext() { }

    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);

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

        builder.Entity<HealthRecord>()
            .HasOne(x => x.Person)
            .WithOne(x => x.HealthRecord)
            .HasForeignKey<HealthRecord>("PersonId");

        InitData(builder);
    }

    private void InitData(ModelBuilder builder) {
        builder.Entity<Drug>().HasData(
            new Drug() {
                Id = 1,
                Name = "Ipren",
                Form = Enums.Form.Tablet,
                ActiveSubstance = "Kokain",
                Effects = [Enums.Effect.Analgesic]
            },
            new Drug() {
                Id = 2,
                Name = "Treo",
                Form = Enums.Form.Tablet,
                ActiveSubstance = "MDMA",
                Effects = [Enums.Effect.Analgesic, Enums.Effect.Anti_Inflammatory, Enums.Effect.Antipyretic]
            },
            new Drug() {
                Id = 3,
                Name = "Viagra",
                Form = Enums.Form.Tablet,
                ActiveSubstance = "Secret",
                Effects = [Enums.Effect.Other]
            },
            new Drug() {
                Id = 4,
                Name = "Amoxicillin",
                Form = Enums.Form.Liquid,
                ActiveSubstance = "Kokain",
                Effects = [Enums.Effect.Antibiotic]
            },
            new Drug() {
                Id = 5,
                Name = "Thomas Energy Supplement",
                Form = Enums.Form.Injection,
                ActiveSubstance = "alpha-methylphenethylamine",
                Effects = [Enums.Effect.Analgesic, Enums.Effect.Other]
            }
            );
        builder.Entity<SymptomType>().HasData(
            new SymptomType() { Id = 1, Name = "Snuva" },
            new SymptomType() { Id = 2, Name = "Hosta" },
            new SymptomType() { Id = 3, Name = "Feber" },
            new SymptomType() { Id = 4, Name = "Huvudvärk" },
            new SymptomType() { Id = 5, Name = "Låg Energi" },
            new SymptomType() { Id = 6, Name = "Nedsatt prestationsförmåga" }
            );

        //builder.Entity<Person>().HasData(
        //    kalle
        //    );
        //builder.Entity<HealthRecord>().HasData(
        //    new HealthRecord { Id = -1, PersonId = kalle.Id }
        //    );
    }
}
