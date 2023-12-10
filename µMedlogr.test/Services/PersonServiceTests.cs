using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using µMedlogr.core;
using µMedlogr.core.Models;
using µMedlogr.core.Services;

namespace µMedlogr.Tests.Unit.Services; 
public class PersonServiceTests {
    private readonly DbContextOptions<µMedlogrContext> _contextOptions;
    private readonly µMedlogrContext _context;
    private MockRepository mockRepository;
    private Mock<µMedlogrContext> mockµMedlogrContext;

    public PersonServiceTests() {
        this.mockRepository = new MockRepository(MockBehavior.Loose);

        this.mockµMedlogrContext = this.mockRepository.Create<µMedlogrContext>();
      _contextOptions = new DbContextOptionsBuilder<µMedlogrContext>()
      .UseInMemoryDatabase("TestDb") //Add Context imdb root
      .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
      .Options;
      ResetDb();
    }

    private PersonService CreateDefaultService() {
        return new PersonService(CreateContext());
    }

    [Fact]
    public async Task Delete_NullUser_ReturnsFalse() {
        // Arrange
        var sut = this.CreateDefaultService();
        Person entity = null!;

        // Act
        var result = await sut.Delete(entity);

        // Assert
        Assert.False(result);
        this.mockRepository.VerifyAll();
    }
    [Fact]
    //[Theory]
    public async Task Find_EntityKeyIsLessThanOne_ReturnsNull() {
        // Arrange
        var sut = this.CreateDefaultService();
        Person expected = null!;
        int key = 0;

        // Act
        var result = await sut.Find(key);

        // Assert
        Assert.Equal(expected, result);
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task GetAll_HasData_ReturnsData() {
        // Arrange
        var sut = this.CreateDefaultService();
        // Act
        var result = await sut.GetAll();

        // Assert
        Assert.NotEmpty(result);
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task SaveAll_ListOfInput_ReturnsTrue() {
        // Arrange
        var sut = this.CreateDefaultService();
        List<Person> values = [new Person() { NickName = "John"}, new Person() { NickName = "Anna" }, new Person() { NickName = "Petter" }];

        // Act
        var result = await sut.SaveAll(values);

        // Assert
        Assert.True(result);
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task Update_UpdatedUserIsNull_ReturnsFalse() {
        // Arrange
        var sut = this.CreateDefaultService();
        Person entity = null!;

        // Act
        var result = await sut.Update(entity);

        // Assert
        Assert.False(result);

        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task DeletePerson_PersonIsNull_ThrowsArgumentNullException() {
        // Arrange
        var sut = this.CreateDefaultService();
        Person person = null;

        // Act
        Task result() =>sut.DeletePerson(person);

        // Assert
        Assert.ThrowsAsync<ArgumentNullException>(result);
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task FindPerson_DefaultUserId_ReturnsNull() {
        // Arrange
        var sut = this.CreateDefaultService();
        int personId = 0;
        Person expected = null;

        // Act
        var result = await sut.FindPerson(personId);

        // Assert
        Assert.Equal(expected, result);
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task GetAppUsersPersonById_StateUnderTest_ExpectedBehavior() {
        // Arrange
        var sut = this.CreateDefaultService();
        string userId = null;

        // Act
        var result = await sut.GetAppUsersPersonById(userId);

        // Assert
        Assert.Equal(null, result);
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task SavePerson_PersonIsNull_ReturnsFalse() {
        // Arrange
        var sut = this.CreateDefaultService();
        Person person = null!;

        // Act
        var result = await sut.SavePerson(person);

        // Assert
        Assert.False(result);
        this.mockRepository.VerifyAll();
    }

    [Fact]
    public async Task UpdatePerson_UpdatedPersonIsNull_ReturnsFalse() {
        // Arrange
        var sut = this.CreateDefaultService();
        Person person = null!;

        // Act
        var result = await sut.UpdatePerson(person);

        // Assert
        Assert.False(result);
        this.mockRepository.VerifyAll();
    }
    [Fact]
    public async Task DeletePerson_ValidPerson_ReturnsTrue()
    {
        // Arrange
        var mockPerson = new Person { Id = 1};
        var mockContext = new Mock<µMedlogrContext>(_contextOptions);
        mockContext.Setup(x => x.People.Remove(It.IsAny<Person>())).Callback<Person>((entity) => {
        });
        mockContext.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1); 

        var sut = new PersonService(mockContext.Object);

        // Act
        var result = await sut.DeletePerson(mockPerson);

        // Assert
        Assert.True(result);
    }
    private µMedlogrContext CreateContext() => new(_contextOptions);
    private void ResetDb() {
        using var context = new µMedlogrContext(_contextOptions);
        // Do not delete between uses
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        context.SaveChanges();
    }
}
