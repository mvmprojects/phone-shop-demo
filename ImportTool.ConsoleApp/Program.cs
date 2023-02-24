using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Phoneshop.Business;
using Phoneshop.Domain.Interfaces;
using System;
using System.IO;

namespace ImportTool.ConsoleApp
{
    internal class Program
    {
        private static IXmlToPhoneService xmlToPhone;
        private static ICaching cache;

        private static void Main(string[] args)
        {
            // path from args via debug launch profile
            string path = args[0];

            string fileToEnd;

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
                .AddScoped<IXmlToPhoneService, XmlToPhoneService>()
                .BuildServiceProvider();

            xmlToPhone = serviceProvider.GetService<IXmlToPhoneService>();

            cache = serviceProvider.GetService<ICaching>();
            cache.SlidingExpSeconds = slidingExpSeconds;
            cache.AbsoluteExpSeconds = absoluteExpSeconds;

            if (File.Exists(path))
            {
                try
                {
                    using var sr = new StreamReader(path);
                    fileToEnd = sr.ReadToEnd();

                    Console.WriteLine("Calling data mapping service...");
                    xmlToPhone.MapImportedPhones(fileToEnd);
                }
                catch (IOException)
                {
                    Console.WriteLine("An I/O error has occurred.");
                    throw;
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("Invalid operation exception.");
                    throw;
                }
                catch (Exception)
                {
                    Console.WriteLine("An unexpected error has occurred.");
                    throw;
                }
            }
            else Console.WriteLine("There was something wrong with the " +
                "file path or user does not have required permissions.");

            Console.WriteLine("\nThis window can now be closed.");
            Console.ReadLine();
        }
    }
}
