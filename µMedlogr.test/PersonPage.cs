using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace µMedlogr.test
{
    public class PersonPage
    {
        [Fact]
        public void CreateList_OfString_ReturnList()
        {
            //arrange
            var sut= µMedlogr.core.Services.PersonPage.CreateAllergiesList();
            //act

            //assert
            Assert.IsType<List<string>>(sut);
        }
        [Fact]
        public void ReturnList_IfEmtpy_WithOneString()
        {
            //arrange
            List<string> myemptyList = new List<string>();
            var sut = µMedlogr.core.Services.PersonPage.ReturnSameListOrAddStringNoAllergy(myemptyList);
            //act
            var actual = "Inga allergier";
            //assert
            Assert.Single(sut);
            Assert.Equal(actual, sut[0]);
            Assert.NotEmpty(sut);

        }
    }
}
