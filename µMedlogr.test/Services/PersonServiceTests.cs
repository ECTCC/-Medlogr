using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using µMedlogr.core;
using µMedlogr.core.Models;
using µMedlogr.core.Services;

namespace µMedlogr.Tests.Unit.Services {
    public class PersonServiceTests {
        private MockRepository mockRepository;

        private Mock<µMedlogrContext> mockµMedlogrContext;

        public PersonServiceTests() {
            this.mockRepository = new MockRepository(MockBehavior.Loose);

            this.mockµMedlogrContext = this.mockRepository.Create<µMedlogrContext>();
        }

        private PersonService CreateService() {
            return new PersonService(
                this.mockµMedlogrContext.Object);
        }

        [Fact]
        public async Task Delete_NullUser_ReturnsFalse() {
            // Arrange
            var service = this.CreateService();
            Person entity = null!;

            // Act
            var result = await service.Delete(entity);

            // Assert
            Assert.False(result);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task Find_StateUnderTest_ExpectedBehavior() {
            // Arrange
            var service = this.CreateService();
            int key = 0;

            // Act
            var result = await service.Find(
                key);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetAll_StateUnderTest_ExpectedBehavior() {
            // Arrange
            var service = this.CreateService();

            // Act
            var result = await service.GetAll();

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task SaveAll_StateUnderTest_ExpectedBehavior() {
            // Arrange
            var service = this.CreateService();
            List<Person> values = null!;

            // Act
            var result = await service.SaveAll(
                values);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task Update_UpdatedUserIsNull_ReturnsFalse() {
            // Arrange
            var service = this.CreateService();
            Person entity = null!;

            // Act
            var result = await service.Update(entity);

            // Assert
            Assert.False(result);

            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task DeletePerson_StateUnderTest_ExpectedBehavior() {
            // Arrange
            var service = this.CreateService();
            Person person = null;

            // Act
            var result = await service.DeletePerson(
                person);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task FindPerson_DefaultUserId_ReturnsNull() {
            // Arrange
            var service = this.CreateService();
            int personId = 0;
            Person expected = null;

            // Act
            var result = await service.FindPerson(personId);

            // Assert
            Assert.Equal(expected, result);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetAppUsersPersonById_StateUnderTest_ExpectedBehavior() {
            // Arrange
            var service = this.CreateService();
            string userId = null;

            // Act
            var result = await service.GetAppUsersPersonById(userId);

            // Assert
            Assert.Equal(null, result);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task SavePerson_PersonIsNull_ReturnsFalse() {
            // Arrange
            var service = this.CreateService();
            Person person = null!;

            // Act
            var result = await service.SavePerson(person);

            // Assert
            Assert.False(result);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task UpdatePerson_UpdatedPersonIsNull_ReturnsFalse() {
            // Arrange
            var service = this.CreateService();
            Person person = null!;

            // Act
            var result = await service.UpdatePerson(person);

            // Assert
            Assert.False(result);
            this.mockRepository.VerifyAll();
        }
    }
}
