using µMedlogr.core;
using µMedlogr.core.Enums;
using µMedlogr.core.Models;
using µMedlogr.core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Moq;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Data.Entity.Infrastructure;

namespace µMedlogr.test;
public class EntityManagerTest
{
    private readonly UserManager<AppUser> _userManager;
    private readonly DbContextOptions<µMedlogrContext> _context;
    public EntityManagerTest(UserManager<AppUser> userManager)
    {
        userManager = _userManager;
        _context = new DbContextOptionsBuilder<µMedlogrContext>()
       .UseInMemoryDatabase("TestDb")
       .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
       .Options;

        using var context = new µMedlogrContext(_context);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        context.AddRange(
            new HealthRecord(){ Id = 1, Temperatures = [],
                PersonId = 1,
                Entries = [],CurrentSymptoms = [],

            });

        context.SaveChanges();
    }
    #region Tests Invariant
    [Fact]
    public void CreateSymptomMeasurement_SymptomIdIsZero_ReturnTaskEvaluatedToNull()
    {
        //Arrange
        var sut = CreateDefaultEntityManager();
        var severity = Severity.Maximal;

        //Act
        var actual = sut.CreateSymptomMeasurement(0, severity);

        //Assert
        Assert.Null(actual.Result);
    }
    [Fact]
    public void CreateSymptomMeasurement_SeverityIsOutOfRange_ThrowsArgumentOutOfRangeException()
    {
        //Arrange
        var sut = CreateDefaultEntityManager();
        //Act
        //Assert
        Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => sut.CreateSymptomMeasurement(1, (Severity)Int32.MaxValue));
    }
    [Fact]
    public void EntityManager_ConstructorWithNullContext_ThrowsArgumentNullException()
    {
        //Arrange
        //Act
        //Assert
        Assert.Throws<ArgumentNullException>(() => new EntityManager(null!,null!));
    }
    [Fact]
    public void SaveSymptomMeasurement_DatabaseDoesNotSaveValidValue_ThrowsDbUpdateException()
    {
        //Arrange
        var optionsbuilder = new DbContextOptionsBuilder<µMedlogrContext>();
        var mock = new Mock<µMedlogrContext>(optionsbuilder.Options);
        mock.Setup(m => m.SaveChangesAsync(default)).ReturnsAsync(0);
        var userManagerMock = new Mock<UserManager<AppUser>>(Mock.Of<IUserStore<AppUser>>(), null, null, null, null, null, null, null, null);
        var sut = new µMedlogr.core.Services.EntityManager(mock.Object,userManagerMock.Object);
        var symptomMeasurement = new SymptomMeasurement
        {
            Id = 0,
            Symptom = new SymptomType { Id = 1, Name = "Snuva", Records = [] },
            SubjectiveSeverity = Severity.Maximal,
        };

        //Act
        //Assert
        Assert.ThrowsAsync<Microsoft.EntityFrameworkCore.DbUpdateException>(() => sut.SaveNewSymptomMeasurement(symptomMeasurement));
    }
    [Fact]
    public void AddTemperatureData_InvalidHealthRecordId_ReturnsFalse()
    {
        //Arrange
        var sut = CreateDefaultEntityManager();
        int invalidId = 0;
        float validTemperature = 42.0f;
        string noNotes = string.Empty;
        //Act
        var result = sut.AddTemperatureData(invalidId, validTemperature, noNotes);

        //Assert
        Assert.False(result);
    }
    #endregion
    #region Tests Variant
    [Theory]
    [MemberData(nameof(ValidTemperatureData))]
    internal void AddTemperatureData_ValidTemperatureParameterData_ReturnsTrue(int healthRecordId, float temperature, string notes)
    {
        //Arrange
        var sut = CreateDefaultEntityManager();
        //Act
        var result = sut.AddTemperatureData(healthRecordId, temperature, notes);
        //Assert
        Assert.True(result);
    }

    //[Theory]
    //[MemberData(nameof(ValidSymptomMeasurementData))]
    //internal void CreateSymptomMeasurement_ValidNewSymptomParameters_ReturnsANewMeasurement(int validSymptomId, Severity severity)
    //{
    //    //Arrange
    //    var optionsbuilder = new DbContextOptionsBuilder<µMedlogrContext>();
    //    var mock = new Mock<µMedlogrContext>(optionsbuilder.Options);
    //    mock.Setup(m => m.SymptomTypes).Returns(
    //        new FakeDbSet<SymptomType> { new SymptomType { Id = validSymptomId, Name = "snuva" } }
    //        );
    //    var sut = new EntityManager(mock.Object);
    //    //Act
    //    var actual = sut.CreateSymptomMeasurement(validSymptomId, severity);
    //    //Assert
    //    Assert.NotNull(actual.Result);
    //}
    [Theory]
    [MemberData(nameof(ValidSymptomMeasurementsNotAlreadyInDatabase))]
    internal void SaveSymptomMeasurement_ValidNewSymptom_ReturnsTrue(SymptomMeasurement validSymptomMeasurement)
    {
        //Arrange
        var optionsbuilder = new DbContextOptionsBuilder<µMedlogrContext>();
        var mock = new Mock<µMedlogrContext>(optionsbuilder.Options);
        var userManagerMock = new Mock<UserManager<AppUser>>(Mock.Of<IUserStore<AppUser>>(), null, null, null, null, null, null, null, null);
        mock.Setup(m => m.SaveChangesAsync(default)).Verifiable();
        mock.Setup(m => m.SaveChangesAsync(default)).ReturnsAsync(1);
        var sut = new µMedlogr.core.Services.EntityManager(mock.Object,userManagerMock.Object);

        //Act
        var actual = sut.SaveNewSymptomMeasurement(validSymptomMeasurement);
        //Assert
        Assert.True(actual.Result);
        mock.Verify(m => m.SaveChangesAsync(default), Times.Once);
    }
    [Theory]
    [MemberData(nameof(ValidSymptomMeasurementsAlreadyInDatabase))]
    internal void SaveSymptomMeasurement_NewSymptomIsAlreadyAnEntity_ReturnsFalse(SymptomMeasurement validSymptomMeasurement)
    {
        //Arrange
        var sut = CreateDefaultEntityManager();

        //Act
        var actual = sut.SaveNewSymptomMeasurement(validSymptomMeasurement);

        //Assert
        Assert.False(actual.Result);
    }

    #endregion
    #region Private
    private EntityManager CreateDefaultEntityManager()
    {
        var optionsbuilder = new DbContextOptionsBuilder<µMedlogrContext>();
        var mock = new Mock<µMedlogrContext>(optionsbuilder.Options);
        using var context = CreateContext();
        var userManagerMock = new Mock<UserManager<AppUser>>(Mock.Of<IUserStore<AppUser>>(), null, null, null, null, null, null, null, null);

        return new µMedlogr.core.Services.EntityManager(context, userManagerMock.Object);
    }

    µMedlogrContext CreateContext() => new µMedlogrContext(_context);

    //private EntityManager CreateEntityManagerInMemoryDb()
    //{
    //    var optionbuilder = new DbContextOptionsBuilder<µMedlogrContext>()
    //         .UseInMemoryDatabase(databaseName: "InMemoryDb")
    //         .Options;
    //}

    //    #endregion
    //    #region TestData
    public static IEnumerable<object[]> ValidSymptomMeasurementData()
    {
        yield return new object[] {
                1,
                Severity.Maximal
                };
        yield return new object[] {
                2,
                Severity.Unbearable
                };
    }
    public static IEnumerable<object[]> InValidSymptomMeasurementData()
    {
        yield return new object[] {
                0,
                Severity.Maximal
                };
        yield return new object[] {
                2,
                Severity.None
        };
        yield return new object[] {
                0,
                Severity.Mild
        };
    }
    public static IEnumerable<object[]> ValidSymptomMeasurementsNotAlreadyInDatabase()
    {
        yield return new object[] {
                new SymptomMeasurement {   Id = 0,
                                    Symptom = new SymptomType { Id=1, Name="Snuva", Records=[] },
                                    SubjectiveSeverity = Severity.Maximal,
                                 
                }
                };
        yield return new object[] {
                new SymptomMeasurement {   Id = 0,
                                    Symptom = new SymptomType { Id=2, Name="Hosta", Records=[] },
                                    SubjectiveSeverity = Severity.Maximal,
                                   
                }
                };
        yield return new object[] {
                new SymptomMeasurement {   Id = 0,
                                    Symptom = new SymptomType { Id=3, Name="Huvudvärk", Records=[] },
                                    SubjectiveSeverity = Severity.Maximal,
                                  
                }
                };

    }
    public static IEnumerable<object[]> ValidSymptomMeasurementsAlreadyInDatabase()
    {
        yield return new object[] {
                new SymptomMeasurement {   Id = 1,
                                    Symptom = new SymptomType { Id=1, Name="Snuva", Records=[] },
                                    SubjectiveSeverity = Severity.Maximal,
                
                }
                };
        yield return new object[] {
                new SymptomMeasurement {   Id = -5,
                                    Symptom = new SymptomType { Id=2, Name="Hosta", Records=[] },
                                    SubjectiveSeverity = Severity.Maximal,
                        
                }
                };
        yield return new object[] {
                new SymptomMeasurement {   Id = 1,
                                    Symptom = new SymptomType { Id=3, Name="Huvudvärk", Records=[] },
                                    SubjectiveSeverity = Severity.Maximal,
                           
                }
                };

    }
    public static IEnumerable<object[]> ValidTemperatureData()
    {

        yield return new object[] {
                1,
                37.0f,
                ""
                };
        yield return new object[] {
                1,
                38.0f,
                "Feber ökar"
                };
    }

    #endregion

}

public class FakeDbSet<T> : DbSet<T> where T : class
{
    public override IEntityType EntityType => throw new NotImplementedException();
}
//public class µMedlogrContext : DbContext
//{
//    internal DbSet<HealthRecord> HealthRecords { get; set; } = default!;
//    internal DbSet<Person> People { get; set; } = default!;
//    internal DbSet<SymptomMeasurement> SymptomMeasurements { get; set; } = default!;
//    internal DbSet<SymptomType> SymptomTypes { get; set; } = default!;
//    internal DbSet<TemperatureData> TemperatureDatas { get; set; } = default!;
//    internal DbSet<HealthRecordEntry> HealthRecordsEntrys { get; set; } = default!;
//    internal DbSet<AppUser> AppUsers { get; set; } = default!;

//    public µMedlogrContext(DbContextOptions options) : base(options) { }
//    protected override void OnModelCreating(ModelBuilder builder)
//    {
//        base.OnModelCreating(builder);

//        builder.Entity<AppUser>()
//            .HasMany(x => x.PeopleInCareOf)
//            .WithMany(x => x.CareGivers)
//            .UsingEntity<Dictionary<string, object>>(
//                "AppUserPerson",
//                j => j
//                .HasOne<AppUser>()
//                .WithMany()
//                .HasForeignKey("CareGiversId")
//                .OnDelete(DeleteBehavior.Restrict)
//                );

//        builder.Entity<AppUser>()
//            .HasOne(x => x.Me);

//        builder.Entity<HealthRecord>()
//            .HasOne(x => x.Person)
//            .WithOne(x => x.HealthRecord)
//            .HasForeignKey<HealthRecord>("PersonId");

//        SeedDataBase(builder);

//    }

//    private void SeedDataBase(ModelBuilder builder)
//    {
//        //Skapa object innan
//        var kalle = new Person { Id = 1, NickName = "Nisse", WeightInKg = 47 };

//        builder.Entity<Person>().HasData(
//            kalle
//            );
//        builder.Entity<HealthRecord>().HasData(
//            new HealthRecord { Id = 1, PersonId = kalle.Id }
//            );
//    }
//}
