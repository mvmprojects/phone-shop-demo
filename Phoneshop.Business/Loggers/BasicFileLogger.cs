using Phoneshop.Domain.Interfaces;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Phoneshop.Business
{
    [ExcludeFromCodeCoverage]
    public class BasicFileLogger : IBasicLogger
    {
        private string _path = string.Empty;

        public BasicFileLogger()
        {
            //_path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + "log.txt";

            _path = AppSettingsReader.GetAppSettings("../../../../../apploggersettings.json")
                .GetSection("AppLoggerSettings:FileLoggerPath").Value + "\\" + "log.txt";

            AddToFile("Starting new log file.");
        }

        private void AddToFile(string message)
        {
            using (StreamWriter writer = File.AppendText(_path))
            {
                writer.Write("{0} {1} - ", DateTime.Now.ToLongTimeString(),
                                DateTime.Now.ToString("yyyy'-'MM'-'dd"));
                writer.WriteLine(message);
            }
        }

        public void LogDebug(string message) => AddToFile("Debug: " + message);
        public void LogError(string message) => AddToFile("Error: " + message);
        public void LogInfo(string message) => AddToFile("Information: " + message);
        public void LogWarning(string message) => AddToFile("Warning: " + message);
    }
}
