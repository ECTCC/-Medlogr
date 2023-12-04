using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace µMedlogr.test
{
    public class PersonPageTest
    {
        [Fact]
        public void CreateList_OfString_ReturnList()
        {
            //arrange

            //act
            var actual= µMedlogr.core.Services.PersonPage.CreateAllergiesList();
            //assert
            Assert.IsType<List<string>>(actual);
        }
        [Fact]
        public void ReturnList_IfEmtpy_WithOneString()
        {
            //arrange
            List<string> myemptyList = new List<string>();
            var expected = "Inga allergier";
            //act
            var actual = µMedlogr.core.Services.PersonPage.ReturnSameListOrAddStringNoAllergy(myemptyList);
            //assert
            Assert.Single(actual);
            Assert.Equal(expected, actual[0]);
            Assert.NotEmpty(actual);

        }
    }
}
