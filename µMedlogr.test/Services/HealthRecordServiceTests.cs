using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using µMedlogr.core;
using µMedlogr.core.Models;
using µMedlogr.core.Services;

namespace µMedlogr.Tests.Unit.Services; 
public class HealthRecordServiceTests {
    private MockRepository mockRepository;

    private Mock<µMedlogrContext> mockµMedlogrContext;

    public HealthRecordServiceTests() {
        this.mockRepository = new MockRepository(MockBehavior.Strict);

        this.mockµMedlogrContext = this.mockRepository.Create<µMedlogrContext>();
    }

    private HealthRecordService CreateService() {
        return new HealthRecordService(
            this.mockµMedlogrContext.Object);
    }


    [Fact]
    public async Task AddSymptomMeasurementToHealthRecord_StateUnderTest_ExpectedBehavior() {
        // Arrange
        var service = this.CreateService();
        HealthRecord record = null;
        HealthRecordEntry measurement = null;

        // Act
        var result = await service.AddSymptomMeasurementToHealthRecord(
            record,
            measurement);

        // Assert
        Assert.True(false);
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task AddTemperatureDataToHealthRecord_StateUnderTest_ExpectedBehavior() {
        // Arrange
        var service = this.CreateService();
        HealthRecord record = null;
        TemperatureData data = null;

        // Act
        var result = await service.AddTemperatureDataToHealthRecord(
            record,
            data);

        // Assert
        Assert.True(false);
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task GetHealthRecordById_StateUnderTest_ExpectedBehavior() {
        // Arrange
        var service = this.CreateService();
        int id = 0;

        // Act
        var result = await service.GetHealthRecordById(
            id);

        // Assert
        Assert.True(false);
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task GetHealthRecordByAppUserId_StateUnderTest_ExpectedBehavior() {
        // Arrange
        var service = this.CreateService();
        string appUserId = null;

        // Act
        var result = await service.GetHealthRecordByAppUserId(
            appUserId);

        // Assert
        Assert.True(false);
        this.mockRepository.VerifyAll();
    }
    [Fact]
    public async Task GetHealthRecordEntriesByHealthRekordId_StateUnderTest_ExpectedBehavior()
    {
        //Arrange
        var service = this.CreateService();
        int id = 0;

        //Act
        var result = await service.GetHealthRecordEntriesByHealthRekordId(
             id);
        //Assert
        Assert.True(false);
        this.mockRepository.VerifyAll();
    }


    [Fact]
    public async Task SaveHealthRecord_StateUnderTest_ExpectedBehavior() {
        // Arrange
        var service = this.CreateService();
        HealthRecord record = null;

        // Act
        var result = await service.SaveHealthRecord(
            record);

        // Assert
        Assert.True(false);
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task AddEventToHealthRecord_StateUnderTest_ExpectedBehavior() {
        // Arrange
        var service = this.CreateService();
        Event @event = null;
        int healthRecordId = 0;

        // Act
        var result = await service.AddEventToHealthRecord(
            @event,
            healthRecordId);

        // Assert
        Assert.True(false);
        this.mockRepository.VerifyAll();
    }
}
