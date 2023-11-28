using µMedlogr.core;
using µMedlogr.core.Enums;
using µMedlogr.core.Models;
using µMedlogr.core.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace µMedlogr.test;
public class EntityManagerTest {
    #region Tests Invariant
    [Fact]
    public void CreateSymptomMeasurement_SympomIsNull_ReturnTaskEvaluatedToNull() {
        //Arrange
        var sut = CreateDefaultEntityManager();

        //Act
        var actual = sut.CreateSymptomMeasurement(null!, Severity.Maximal);

        //Assert
        Assert.Null(actual.Result);
    }
    #endregion
    #region Tests Variant
    [Theory]
    [MemberData(nameof(ValidSymptomMeasurementData))]
    internal void CreateSymptomMeasurement_ValidNewSymptomParameters_ReturnsANewMeasurement(SymptomType symptom, Severity severity) {
        //Arrange
        var sut = CreateDefaultEntityManager();
        //Act
        var actual = sut.CreateSymptomMeasurement(symptom, severity);
        //Assert
        Assert.NotNull(actual.Result);
    }
    #endregion
    #region Private
    private EntityManager CreateDefaultEntityManager() {
        var optionsbuilder = new DbContextOptionsBuilder<µMedlogrContext>();
        var mock = new Mock<µMedlogrContext>(optionsbuilder.Options);
        return new µMedlogr.core.Services.EntityManager(mock.Object);
    }
    #endregion
    #region TestData
    public static IEnumerable<object[]> ValidSymptomMeasurementData() {
        yield return new object[] {
                new SymptomType {   Id = 1,
                                    Name = "Snuva",
                                    Records = new List<HealthRecord>()
                },
                Severity.Maximal
                };
        yield return new object[] {
            new SymptomType {   Id = 2,
                                    Name = "Hosta",
                                    Records = new List<HealthRecord>()
                },
                Severity.Unbearable
        };
    }
    public static IEnumerable<object[]> InValidSymptomMeasurementData() {
        yield return new object[] {
                new SymptomType {   Id = 0,
                                    Name = null,
                                    Records = null!
                },
                Severity.Maximal
                };
        yield return new object[] {
            new SymptomType {   Id = 2,
                                    Name = "Symptom2",
                                    Records = new List<HealthRecord>()
                },
                Severity.None
        };
    }
    #endregion
}