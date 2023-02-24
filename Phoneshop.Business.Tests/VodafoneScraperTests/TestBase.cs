using Phoneshop.Business.Scrapers;

namespace Phoneshop.Business.Tests.VodafoneScraperTests
{
    public class TestBase
    {
        public VodafoneScraper _scraper;

        public TestBase()
        {
            _scraper = new VodafoneScraper();
        }
    }
}
