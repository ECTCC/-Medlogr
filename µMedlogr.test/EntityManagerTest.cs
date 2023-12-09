using µMedlogr.core;
using µMedlogr.core.Enums;
using µMedlogr.core.Models;
using µMedlogr.core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;

namespace µMedlogr.Tests.Unit;
public class EntityManagerTest {
    private readonly DbContextOptions<µMedlogrContext> _contextOptions;
    public EntityManagerTest() {
        _contextOptions = new DbContextOptionsBuilder<µMedlogrContext>()
       .UseInMemoryDatabase("TestDb") //Add Context imdb root
       .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
       .Options;
        ResetDb();
        SeedTestData();
    }

    #region Tests Invariant
    [Fact]
    [Trait("Category", "Unit")]
    public void CreateSymptomMeasurement_SymptomIdIsZero_ReturnTaskEvaluatedToNull() {
        //Arrange
        var sut = CreateEntityManagerWithMockedDbContext();
        var severity = Severity.Maximal;

        //Act
        var actual = sut.CreateSymptomMeasurement(0, severity);

        //Assert
        Assert.Null(actual.Result);
    }
    [Fact]
    [Trait("Category", "Unit")]
    public void CreateSymptomMeasurement_SeverityIsOutOfRange_ThrowsArgumentOutOfRangeException() {
        //Arrange
        var sut = CreateDefaultEntityManager();
        //Act
        //Assert
        Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => sut.CreateSymptomMeasurement(1, (Severity)int.MaxValue));
    }
    [Fact]
    [Trait("Category", "Unit")]
    public void EntityManager_ConstructorWithNullContext_ThrowsArgumentNullException() {
        //Arrange
        //Act
        //Assert
        Assert.Throws<ArgumentNullException>(() => new EntityManager(null!, null!));
    }
    [Fact]
    [Trait("Category", "Unit")]
    public void SaveSymptomMeasurement_DatabaseDoesNotSaveValidValue_ThrowsDbUpdateException() {
        //Arrange
        var optionsbuilder = new DbContextOptionsBuilder<µMedlogrContext>();
        var mock = new Mock<µMedlogrContext>(optionsbuilder.Options);
        mock.Setup(m => m.SaveChangesAsync(default)).ReturnsAsync(0);
        var userManagerMock = DefaultUserManagerMock();
        var sut = new EntityManager(mock.Object, userManagerMock.Object);
        var symptomMeasurement = new SymptomMeasurement {
            Id = 0,
            Symptom = new SymptomType { Id = 1, Name = "Snuva", Records = [] },
            SubjectiveSeverity = Severity.Maximal,
        };

        //Act
        //Assert
        Assert.ThrowsAsync<DbUpdateException>(() => sut.SaveNewSymptomMeasurement(symptomMeasurement));
    }
    [Fact]
    [Trait("Category", "Unit")]
    public void AddTemperatureData_InvalidHealthRecordId_ReturnsFalse() {
        //Arrange
        var sut = CreateEntityManagerWithMockedDbContext();
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
    [Trait("Category", "Unit")]
    [MemberData(nameof(ValidTemperatureData))]
    internal void AddTemperatureData_ValidTemperatureParameterData_ReturnsTrue(int healthRecordId, float temperature, string notes) {
        //Arrange
        var sut = CreateDefaultEntityManager();
        //Act
        var result = sut.AddTemperatureData(healthRecordId, temperature, notes);
        //Assert
        Assert.True(result);
    }

    [Theory]
    [Trait("Category", "Unit")]
    [MemberData(nameof(ValidSymptomMeasurementData))]
    internal void CreateSymptomMeasurement_ValidNewSymptomParameters_ReturnsANewMeasurement(int validSymptomId, Severity severity) {
        //Arrange
        var sut = CreateDefaultEntityManager();
        //Act
        var actual = sut.CreateSymptomMeasurement(validSymptomId, severity);
        //Assert
        Assert.NotNull(actual.Result);
    }
    [Theory]
    [Trait("Category", "Unit")]
    [MemberData(nameof(ValidSymptomMeasurementsNotAlreadyInDatabase))]
    internal void SaveSymptomMeasurement_ValidNewSymptom_ReturnsTrue(SymptomMeasurement validSymptomMeasurement) {
        //Arrange
        var optionsbuilder = new DbContextOptionsBuilder<µMedlogrContext>();
        var mock = new Mock<µMedlogrContext>(optionsbuilder.Options);
        var userManagerMock = DefaultUserManagerMock();
        mock.Setup(m => m.SaveChangesAsync(default)).Verifiable();
        mock.Setup(m => m.SaveChangesAsync(default)).ReturnsAsync(1);
        var sut = new EntityManager(mock.Object, userManagerMock.Object);

        //Act
        var actual = sut.SaveNewSymptomMeasurement(validSymptomMeasurement);
        //Assert
        Assert.True(actual.Result);
        mock.Verify(m => m.SaveChangesAsync(default), Times.Once);
    }
    [Theory]
    [Trait("Category", "Unit")]
    [MemberData(nameof(ValidSymptomMeasurementsAlreadyInDatabase))]
    internal void SaveSymptomMeasurement_NewSymptomIsAlreadyAnEntity_ReturnsFalse(SymptomMeasurement validSymptomMeasurement) {
        //Arrange
        var sut = CreateDefaultEntityManager();

        //Act
        var actual = sut.SaveNewSymptomMeasurement(validSymptomMeasurement);

        //Assert
        Assert.False(actual.Result);
    }

    #endregion
    #region Private
    private EntityManager CreateDefaultEntityManager() {
        var context = CreateContext();
        Mock<UserManager<AppUser>> userManagerMock = DefaultUserManagerMock();

        return new EntityManager(context, userManagerMock.Object);
    }

    private static Mock<UserManager<AppUser>> DefaultUserManagerMock() {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        return new Mock<UserManager<AppUser>>(Mock.Of<IUserStore<AppUser>>(), null, null, null, null, null, null, null, null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    }

    private EntityManager CreateEntityManagerWithMockedDbContext() {
        var optionsbuilder = new DbContextOptionsBuilder<µMedlogrContext>();
        var mock = new Mock<µMedlogrContext>(optionsbuilder.Options);
        var userManagerMock = DefaultUserManagerMock();

        return new EntityManager(mock.Object, userManagerMock.Object);
    }

    private µMedlogrContext CreateContext() => new(_contextOptions);

    private void ResetDb() {
        using var context = new µMedlogrContext(_contextOptions);
        // Do not delete between uses
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        context.SaveChanges();
    }

    private void SeedTestData() {
        using var context = new µMedlogrContext(_contextOptions);

        context.AddRange(
            new HealthRecord() {
                Id = 1,
                Temperatures = [],
                PersonId = 1,
                Entries = [],
                CurrentSymptoms = [],
            });

        context.SaveChanges();
    }

    #endregion
    #region TestData
    public static IEnumerable<object[]> ValidSymptomMeasurementData() {
        yield return new object[] {
                1,
                Severity.Maximal
                };
        yield return new object[] {
                2,
                Severity.Unbearable
                };
    }
    public static IEnumerable<object[]> InValidSymptomMeasurementData() {
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
    public static IEnumerable<object[]> ValidSymptomMeasurementsNotAlreadyInDatabase() {
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
    public static IEnumerable<object[]> ValidSymptomMeasurementsAlreadyInDatabase() {
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
    public static IEnumerable<object[]> ValidTemperatureData() {

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