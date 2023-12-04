//using µMedlogr.core;
//using µMedlogr.core.Enums;
//using µMedlogr.core.Models;
//using µMedlogr.core.Services;
//using Microsoft.EntityFrameworkCore;
//using Moq;
//using Xunit.Sdk;
//using Microsoft.Extensions.Options;
//using Microsoft.EntityFrameworkCore.Metadata;

//namespace µMedlogr.test;
//public class EntityManagerTest {
//    #region Tests Invariant
//    [Fact]
//    public void CreateSymptomMeasurement_SymptomIdIsZero_ReturnTaskEvaluatedToNull() {
//        //Arrange
//        //var sut = CreateDefaultEntityManager();
//        //var severity = Severity.Maximal;


//        //Act
//        //var actual = sut.CreateSymptomMeasurement(0, severity);

//        //Assert
//        //Assert.Null(actual.Result);
//    }
//    [Fact]
//    public void CreateSymptomMeasurement_SeverityIsOutOfRange_ThrowsArgumentOutOfRangeException() {
//        //Arrange
//        //var sut = CreateDefaultEntityManager();
//        //Act
//        //Assert
//        //Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => sut.CreateSymptomMeasurement(1, (Severity)Int32.MaxValue));
//    }
//    [Fact]
//    public void EntityManager_ConstructorWithNullContext_ThrowsArgumentNullException() {
//        //Arrange
//        //Act
//        //Assert
//        //Assert.Throws<ArgumentNullException>(() => new EntityManager(null!));
//    }
//    [Fact]
//    public void SaveSymptomMeasurement_DatabaseDoesNotSaveValidValue_ThrowsDbUpdateException() {
//        //Arrange
//        //var optionsbuilder = new DbContextOptionsBuilder<µMedlogrContext>();
//        //var mock = new Mock<µMedlogrContext>(optionsbuilder.Options);
//        //mock.Setup(m => m.SaveChangesAsync(default)).ReturnsAsync(0);
//        //var sut = new µMedlogr.core.Services.EntityManager(mock.Object);
//        //var symptomMeasurement = new SymptomMeasurement {
//        //    Id = 0,
//        //    Symptom = new SymptomType { Id = 1, Name = "Snuva", Records = [] },
//        //    SubjectiveSeverity = Severity.Maximal,
//        //    TimeSymptomWasChecked = DateTime.Now,
//        //};

//        //Act
//        //Assert
//        //Assert.ThrowsAsync<DbUpdateException>(() => sut.SaveNewSymptomMeasurement(symptomMeasurement));
//    }
//    #endregion
//    #region Tests Variant
//    [Theory]
//    [MemberData(nameof(ValidSymptomMeasurementData))]
//    internal void CreateSymptomMeasurement_ValidNewSymptomParameters_ReturnsANewMeasurement(int validSymptomId, Severity severity) {
//        //Arrange
//        //var optionsbuilder = new DbContextOptionsBuilder<µMedlogrContext>();
//        //var mock = new Mock<µMedlogrContext>(optionsbuilder.Options);
//        //mock.Setup(m => m.SymptomTypes).Returns(
//        //    new FakeDbSet<SymptomType> { new SymptomType { Id = validSymptomId, Name = "snuva" } }
//        //    );
//        //var sut = new EntityManager(mock.Object);
//        //Act
//        //var actual = sut.CreateSymptomMeasurement(validSymptomId, severity);
//        //Assert
//        //Assert.NotNull(actual.Result);
//    }
//    [Theory]
//    [MemberData(nameof(ValidSymptomMeasurementsNotAlreadyInDatabase))]
//    internal void SaveSymptomMeasurement_ValidNewSymptom_ReturnsTrue(SymptomMeasurement validSymptomMeasurement) {
//        //Arrange
//        //var optionsbuilder = new DbContextOptionsBuilder<µMedlogrContext>();
//        //var mock = new Mock<µMedlogrContext>(optionsbuilder.Options);
//        //mock.Setup(m => m.SaveChangesAsync(default)).Verifiable();
//        //mock.Setup(m => m.SaveChangesAsync(default)).ReturnsAsync(1);
//        //var sut = new µMedlogr.core.Services.EntityManager(mock.Object);

//        //Act
//        //var actual = sut.SaveNewSymptomMeasurement(validSymptomMeasurement);
//        //Assert
//        //Assert.True(actual.Result);
//        //mock.Verify(m => m.SaveChangesAsync(default), Times.Once);
//    }
//    [Theory]
//    [MemberData(nameof(ValidSymptomMeasurementsAlreadyInDatabase))]
//    internal void SaveSymptomMeasurement_NewSymptomIsAlreadyAnEntity_ReturnsFalse(SymptomMeasurement validSymptomMeasurement) {
//        //Arrange
//        //var sut = CreateDefaultEntityManager();

//        //Act
//        //var actual = sut.SaveNewSymptomMeasurement(validSymptomMeasurement);

//        //Assert
//        //Assert.False(actual.Result);
//    }

//    #endregion
//    #region Private
//    //private EntityManager CreateDefaultEntityManager() {
//        //var optionsbuilder = new DbContextOptionsBuilder<µMedlogrContext>();
//        //var mock = new Mock<µMedlogrContext>(optionsbuilder.Options);
//        //return new µMedlogr.core.Services.EntityManager(mock.Object);
//    //}

//    #endregion
////    #region TestData
////    public static IEnumerable<object[]> ValidSymptomMeasurementData() {
////        yield return new object[] {
////                1,
////                Severity.Maximal
////                };
////        yield return new object[] {
////                2,
////                Severity.Unbearable
////                };
////    }
////    public static IEnumerable<object[]> InValidSymptomMeasurementData() {
////        yield return new object[] {
////                0,
////                Severity.Maximal
////                };
////        yield return new object[] {
////                2,
////                Severity.None
////        };
////        yield return new object[] {
////                0,
////                Severity.Mild
////        };
////    }
////    public static IEnumerable<object[]> ValidSymptomMeasurementsNotAlreadyInDatabase() {
////        yield return new object[] {
////                new SymptomMeasurement {   Id = 0,
////                                    Symptom = new SymptomType { Id=1, Name="Snuva", Records=[] },
////                                    SubjectiveSeverity = Severity.Maximal,
////                                    TimeSymptomWasChecked = DateTime.Now,
////                }
////                };
////        yield return new object[] {
////                new SymptomMeasurement {   Id = 0,
////                                    Symptom = new SymptomType { Id=2, Name="Hosta", Records=[] },
////                                    SubjectiveSeverity = Severity.Maximal,
////                                    TimeSymptomWasChecked = DateTime.Now,
////                }
////                };
////        yield return new object[] {
////                new SymptomMeasurement {   Id = 0,
////                                    Symptom = new SymptomType { Id=3, Name="Huvudvärk", Records=[] },
////                                    SubjectiveSeverity = Severity.Maximal,
////                                    TimeSymptomWasChecked = DateTime.Now,
////                }
////                };

////    }
////    public static IEnumerable<object[]> ValidSymptomMeasurementsAlreadyInDatabase() {
////        yield return new object[] {
////                new SymptomMeasurement {   Id = 1,
////                                    Symptom = new SymptomType { Id=1, Name="Snuva", Records=[] },
////                                    SubjectiveSeverity = Severity.Maximal,
////                                    TimeSymptomWasChecked = DateTime.Now,
////                }
////                };
////        yield return new object[] {
////                new SymptomMeasurement {   Id = -5,
////                                    Symptom = new SymptomType { Id=2, Name="Hosta", Records=[] },
////                                    SubjectiveSeverity = Severity.Maximal,
////                                    TimeSymptomWasChecked = DateTime.Now,
////                }
////                };
////        yield return new object[] {
////                new SymptomMeasurement {   Id = 1,
////                                    Symptom = new SymptomType { Id=3, Name="Huvudvärk", Records=[] },
////                                    SubjectiveSeverity = Severity.Maximal,
////                                    TimeSymptomWasChecked = DateTime.Now,
////                }
////                };

////    }
////    #endregion

////}

////public class FakeDbSet<T> : DbSet<T> where T : class {
////    public override IEntityType EntityType => throw new NotImplementedException();
////}