using Xunit;

namespace Phoneshop.Business.Tests.VodafoneScraperTests
{
    public class CanExecute : TestBase
    {
        [Fact]
        public void Should_ReturnTrue_When_GivenCorrectURL()
        {
            string url = "https://www.vodafone.nl/telefoon/alle-telefoons?";
            var success = _scraper.CanExecute(url);

            Assert.True(success);
        }
    }
}
