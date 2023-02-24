using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Phoneshop.Domain.Interfaces;
using Phoneshop.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Phoneshop.Business.Scrapers;

public class BelsimpelScraper : IScraper
{
    private readonly string _filePath = "belsimpel";

    public bool CanExecute(string url) => url.Contains(_filePath);

    public List<Phone> ExecuteFile(string path, int consoleLine)
    {
        List<Phone> list = new();

        HtmlDocument doc = new();
        doc.Load(path);

        var nodes = doc.DocumentNode.SelectNodes("//div[@id='beastResultsContainer']//section//h3/a");

        if (nodes == null || nodes.Count == 0) return list;

        var prices = doc.DocumentNode.SelectNodes("//div[@id='beastResultsContainer']//span[@class='Pricingstyle__StyledPricing-sc-1gsjfe6-0 boMCpI']");
        int index = 0;
        foreach (var node in nodes)
        {
            string fullName = node.InnerText;
            var splitName = fullName.Split(' ', 2);
            decimal price = 0;
            var priceRaw = prices[index].InnerText;
            if (decimal.TryParse(priceRaw, out decimal result)) price = result;
            list.Add(
            new()
            {
                Brand = new() { Name = splitName[0] },
                Type = splitName[1],
                Price = price
            });
            index++;
        }
        return list;
    }

    public async Task<List<Phone>> Execute(string url)
    {
        List<Phone> list = new();

        var options = new ChromeOptions();
        options.AddArgument("headless");

        var driver = new ChromeDriver(@"C:\ChromeDriver\", options);

        driver.Navigate().GoToUrl(url);

        await Task.Delay(1000);

        var nodes = driver.FindElements(By.XPath("//div[@id='beastResultsContainer']//section//h3/a"));
        if (nodes == null || nodes.Count == 0)
        {
            driver.Quit();
            return list;
        }
        foreach (var node in nodes)
        {

            string fullName = node.Text;
            var splitName = fullName.Split(' ', 2);

            var priceNode = driver.FindElement(
                By.XPath("//div[@id='beastResultsContainer']//span[@class='Pricingstyle__StyledPricing-sc-1gsjfe6-0 boMCpI']"));

            decimal price = 0;
            var splitPrice = priceNode.Text;
            var priceRaw = splitPrice.Trim();
            if (decimal.TryParse(priceRaw, out decimal result)) price = result;

            list.Add(new()
            {
                Type = splitName[1],
                Brand = new() { Name = splitName[0] },
                Price = price
            });
        }
        driver.Quit();

        return list;
    }
}
