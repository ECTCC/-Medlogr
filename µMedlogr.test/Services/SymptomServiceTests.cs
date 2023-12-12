using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using µMedlogr.core;
using µMedlogr.core.Models;
using µMedlogr.core.Services;

namespace µMedlogr.Tests.Unit.Services {
    public class SymptomServiceTests {
        private MockRepository mockRepository;

        private Mock<µMedlogrContext> mockµMedlogrContext;

        public SymptomServiceTests() {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockµMedlogrContext = this.mockRepository.Create<µMedlogrContext>();
        }

        private SymptomService CreateService() {
            return new SymptomService(
                this.mockµMedlogrContext.Object);
        }

        [Fact]
        public async Task Delete_StateUnderTest_ExpectedBehavior() {
            // Arrange
            var service = this.CreateService();
            SymptomType entity = null;

            // Act
            var result = await service.Delete(
                entity);

            // Assert
            Assert.True(false);
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
            List<SymptomType> values = null;

            // Act
            var result = await service.SaveAll(
                values);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task Update_StateUnderTest_ExpectedBehavior() {
            // Arrange
            var service = this.CreateService();
            SymptomType entity = null;

            // Act
            var result = await service.Update(
                entity);

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task GetAllSymptoms_StateUnderTest_ExpectedBehavior() {
            // Arrange
            var service = this.CreateService();

            // Act
            var result = await service.GetAllSymptoms();

            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }
    }
}
