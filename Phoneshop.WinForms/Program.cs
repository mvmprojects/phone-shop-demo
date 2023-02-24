using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Phoneshop.Business;
using Phoneshop.Domain.Interfaces;
using System;
using System.Windows.Forms;

namespace Phoneshop.WinForms
{
    internal static class Program
    {
        private static IPhoneService phoneService;
        private static ICaching cache;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
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

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PhoneOverview(phoneService));
        }
    }
}
