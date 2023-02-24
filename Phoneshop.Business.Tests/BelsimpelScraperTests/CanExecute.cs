using Xunit;

namespace Phoneshop.Business.Tests.BelsimpelScraperTests;

public class CanExecute : TestBase
{
    [Fact]
    public void Should_ReturnTrue_When_UrlIsFound()
    {
        //Arrange
        string filePath = "../../../../../belsimpel.html";

        //Act
        bool hasExecuted = _scraper.CanExecute(filePath);

        //Assert
        Assert.True(hasExecuted);

    }

    [Fact]
    public void Should_ReturnFalse_When_NoUrlIsFound()
    {
        //Arrange
        string filePath = "";

        //Act
        bool hasExecuted = _scraper.CanExecute(filePath);

        //Assert
        Assert.False(hasExecuted);

    }
}
