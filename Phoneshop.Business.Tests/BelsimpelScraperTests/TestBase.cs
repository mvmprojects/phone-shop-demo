using Phoneshop.Business.Scrapers;

namespace Phoneshop.Business.Tests.BelsimpelScraperTests;

public class TestBase
{
    public BelsimpelScraper _scraper;

    public TestBase() => _scraper = new BelsimpelScraper();
}