using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Phoneshop.Domain.Interfaces;
using Phoneshop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Phoneshop.Business.Scrapers
{
    public class VodafoneScraper : IScraper
    {
        public bool CanExecute(string url) => url.Contains("vodafone");

        public List<Phone> ExecuteFile(string path, int consoleLine)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Path parameter was null or empty.");
            }

            List<Phone> list = new();

            HtmlDocument doc = new();
            doc.Load(path);

            var labels = doc.DocumentNode.SelectNodes(
                "//span[@class='vfz-vodafone-product-card__model-label']");

            var prices = doc.DocumentNode.SelectNodes(
                "//p[@class='vfz-vodafone-product-card__price-container-price']");

            for (int i = 0; i < labels.Count; i++)
            {
                string trimmed = labels[i].InnerText.Trim();
                var split = trimmed.Split(' ', 2);
                string brandName = split[0];
                string typeName = split[1];

                // split "Vanaf € 696,00" to grab the number
                // TODO just split on the euro sign and then trim
                decimal price = 0;
                var splitPrice = prices[0].InnerText.Trim().Split(' ', 3);
                var priceRaw = splitPrice[2];
                if (decimal.TryParse(priceRaw, out decimal result))
                {
                    price = result;
                }

                list.Add(new Phone
                {
                    Type = typeName,
                    Brand = new Brand() { Name = brandName },
                    Description = labels[i].InnerText,
                    Price = price,
                    Stock = 1
                });
            }

            return list;
        }

        [ExcludeFromCodeCoverage]
        public async Task<List<Phone>> Execute(string url)
        {
            // setup
            List<Phone> list = new();

            var options = new ChromeOptions();
            options.AddArgument("headless");

            // https://chromedriver.storage.googleapis.com/index.html?path=105.0.5195.52/
            var driver = new ChromeDriver(@"C:\ChromeDriver\", options);

            // run and wait for javascript to load
            driver.Navigate().GoToUrl(url);

            // selenium wait methods appear to be insufficient - adding Task.Delay()
            await Task.Delay(1000);

            // selenium wait methods added on top
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));

            var labels = wait.Until(e => e.FindElements(
                By.XPath("//span[@class='vfz-vodafone-product-card__model-label']")));

            var prices = wait.Until(e => e.FindElements(
                By.XPath("//p[@class='vfz-vodafone-product-card__price-container-price']")));

            driver.Quit();

            if (labels == null || labels.Count == 0)
            {
                //driver.Quit();
                return list;
            }

            // collect data from elements
            for (int i = 0; i < labels.Count; i++)
            {
                string trimmed = labels[i].Text.Trim();
                var split = trimmed.Split(' ', 2);
                string brandName = split[0];
                string typeName = "";
                if (split.Length > 1)
                {
                    typeName = split[1];
                }

                // split "Vanaf € 696,00" to grab the number
                // TODO just split on the euro sign and then trim
                decimal price = 0;
                var splitPrice = prices[0].Text.Trim().Split(' ', 3);
                var priceRaw = splitPrice[2];
                if (decimal.TryParse(priceRaw, out decimal result))
                {
                    price = result;
                }

                list.Add(new Phone
                {
                    Type = typeName,
                    Brand = new Brand() { Name = brandName },
                    Description = labels[i].Text,
                    Price = price,
                    Stock = 1
                });
            }

            //driver.Quit();
            return list;
        }
    }
}
