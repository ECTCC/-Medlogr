using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using µMedlogr.core;
using µMedlogr.core.Services;

namespace µMedlogr.Tests.Unit.Services;
public class DrugServiceTests {
    private readonly DbContextOptions<µMedlogrContext> _contextOptions;
    private readonly µMedlogrContext _context;
    private MockRepository mockRepository;
    private Mock<µMedlogrContext> mockµMedlogrContext;

    public DrugServiceTests() {
        this.mockRepository = new MockRepository(MockBehavior.Loose);

        this.mockµMedlogrContext = this.mockRepository.Create<µMedlogrContext>();
        _contextOptions = new DbContextOptionsBuilder<µMedlogrContext>()
        .UseInMemoryDatabase("TestDb") //Add Context imdb root
        .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
        .Options;
        ResetDb();
    }

    private DrugService CreateService() {
        return new DrugService(CreateContext());
    }



    [Fact]
    public async Task GetAllDrugs_StateUnderTest_ExpectedBehavior() {
        // Arrange
        var sut = this.CreateService();

        // Act
        var result = await sut.GetAllDrugs();

        // Assert
        Assert.True(false);
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task FindRange_StateUnderTest_ExpectedBehavior() {
        // Arrange
        var sut = this.CreateService();
        List<int> ids = [];

        // Act
        var result = await sut.FindRange(ids);

        // Assert
        Assert.True(false);
        this.mockRepository.VerifyAll();
    }

    private void ResetDb() {
        using var context = new µMedlogrContext(_contextOptions);
        // Do not delete between uses
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        context.SaveChanges();
    }
    private µMedlogrContext CreateContext() => new(_contextOptions);
}
