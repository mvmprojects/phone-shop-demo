using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Phoneshop.Business;
using Phoneshop.Business.Extensions;
using Phoneshop.Domain.Interfaces;
using Phoneshop.Domain.Models;
using System;

namespace Phoneshop.ConsoleApp
{
    internal class Program
    {
        private static IPhoneService phoneService;
        private static ICaching cache;

        private static void Main()
        {
            string conn = AppSettingsReader.GetAppSettings()
                .GetSection("ConnectionStrings:DatabaseConnection").Value;

            int slidingExpSeconds = int.Parse(AppSettingsReader.GetAppSettings()
                .GetSection("ExpirationPolicies:SlidingExpirationSeconds").Value);
            int absoluteExpSeconds = int.Parse(AppSettingsReader.GetAppSettings()
                .GetSection("ExpirationPolicies:AbsoluteExpirationSeconds").Value);

            var serviceProvider = new ServiceCollection()
                .AddDbContext<DataContext>(options => options.UseSqlServer(conn))
                .AddScoped<IPhoneService, PhoneService>()
                .AddScoped<IBrandService, BrandService>()
                .AddScoped(typeof(IRepository<>), typeof(Repository<>))
                .AddSingleton<ICaching, Caching>()
                .AddScoped<IBasicLogger, BasicFileLogger>()
                //.AddScoped<IBasicLogger, DbLogger>()

                .BuildServiceProvider();

            phoneService = serviceProvider.GetService<IPhoneService>();

            cache = serviceProvider.GetService<ICaching>();
            cache.SlidingExpSeconds = slidingExpSeconds;
            cache.AbsoluteExpSeconds = absoluteExpSeconds;

            MainMenu();
        }

        private static void WLine(string s) => Console.WriteLine(s);

        private static void MainMenu()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string pressAnyKeyMsg = "\n Press any key to return to main menu.";

            var phoneList = phoneService.GetPhones();
            Phone[] phoneArray = new Phone[phoneList.Count];

            int i = 0;
            foreach (Phone phone in phoneList)
            {
                phoneArray[i] = phone;
                i++;
            }

            while (true)
            {
                ShowMain(phoneArray);

                var input = Console.ReadLine();

                if ((Int32.TryParse(input, out int result)) &&
                    int.Parse(input) > 0 &&
                    int.Parse(input) <= phoneArray.Length)
                {
                    int id = phoneArray[result - 1].Id;
                    ShowPhone(id);
                }
                else if (input == "s")
                {
                    SearchMenu();
                }
                else
                {
                    WLine("\n Please enter a number that matches an item" +
                        " in the list, or use the search menu.");
                }
                WLine(pressAnyKeyMsg);
                Console.ReadKey();
                Console.Clear();
            }
        }

        private static void SearchMenu()
        {
            Console.Clear();
            WLine("Enter your search term below.");
            var query = Console.ReadLine();
            var results = phoneService.Search(query);

            if (results.Count < 1)
            {
                WLine("\n No items match your search term.");
            }

            foreach (var item in results)
            {
                WLine($"\n {item.Brand.Name} - {item.Type}");
            }
        }

        private static void ShowMain(Phone[] phoneArray)
        {
            int index = 0;

            WLine("Index - Brand - Type");

            foreach (Phone phone in phoneArray)
            {
                Console.Write((index + 1) + " - ");
                WLine($"{phone.Brand.Name} - {phone.Type}\n");
                index++;
            }

            WLine("\n Choose an item from the list by its index number." +
                "\n Alternatively, type s and hit enter to open a search menu.");
        }

        private static void ShowPhone(int input)
        {
            var item = phoneService.GetPhone(input);

            if (item != null)
            {
                WriteDetails(item);
            }
            else
            {
                WLine("\n Entry not found in list.");
            }
        }

        private static void WriteDetails(Phone phone)
        {
            Console.Clear();
            WLine(
                $"Brand: {phone.Brand.Name} - " +
                $"Type: {phone.Type} - " +
                $"Price: {phone.Price:C} - " +
                $"Before VAT: {phone.PriceWithoutVAT():C} - " +
                $"Stock: {phone.Stock}" +
                $"\n {phone.Description}");
        }
    }
}
