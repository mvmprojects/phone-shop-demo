using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Phoneshop.Business
{
    [ExcludeFromCodeCoverage]
    public static class AppSettingsReader
    {
        private static readonly string _staticJsonPath = "../../../../../appsettings.json";

        public static IConfigurationRoot GetAppSettings()
        {
            return BuildConfig(_staticJsonPath);
        }

        public static IConfigurationRoot GetAppSettings(string jsonPath)
        {
            return BuildConfig(jsonPath);
        }

        private static IConfigurationRoot BuildConfig(string path)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile(
                @Directory.GetCurrentDirectory() + path);

            return builder.Build();
        }
    }
}
