using Microsoft.EntityFrameworkCore;
using Moq;
using µMedlogr.core;
using µMedlogr.core.Enums;

namespace µMedlogr.test
{
    public class EntityManagerTest
    {
        [Fact]
        public void CreateSymptomMeasurement_SympomIsNull_ReturnNull()
        {
            //Arrange
            var mock = new Mock<µMedlogrContext>(); 
            var sut = new µMedlogr.core.Services.EntityManager(mock.Object);

            //Act
            var actual = sut.CreateSymptomMeasurement(null,Severity.Maximal);
            //Assert
            Assert.Null(actual);
        }
    }
}