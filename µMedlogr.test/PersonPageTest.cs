namespace µMedlogr.Tests.Unit;
public class PersonPageTest {
    [Fact]
    [Trait("Category", "Unit")]
    public void CreateList_OfString_ReturnList() {
        //arrange

        //act
        var actual = core.Services.PersonPage.CreateAllergiesList();
        //assert
        Assert.IsType<List<string>>(actual);
    }
    [Fact]
    [Trait("Category", "Unit")]
    public void ReturnList_IfEmtpy_WithOneString() {
        //arrange
        List<string> myemptyList = new List<string>();
        var expected = "Inga allergier";
        //act
        var actual = core.Services.PersonPage.ReturnSameListOrAddStringNoAllergy(myemptyList);
        //assert
        Assert.Single(actual);
        Assert.Equal(expected, actual[0]);
        Assert.NotEmpty(actual);
    }
}
