using Phoneshop.Domain.Enums;
using Phoneshop.Domain.Interfaces;
using Phoneshop.Domain.Models;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Phoneshop.Business
{
    [ExcludeFromCodeCoverage]
    public class DbLogger : IBasicLogger
    {
        private readonly IRepository<Log> _repo;

        public DbLogger(IRepository<Log> repository)
        {
            _repo = repository;
        }

        // Logging levels have been tied to an enum called LogLevel.
        // NOTE: db log entries should be moved to a separate database
        // that can deal with constant write actions.
        public void CreateLog(int logLevel, string message)
        {
            Log log = new()
            {
                Level = Enum.GetName(typeof(LogLevel), logLevel),
                Message = message,
                CreatedOn = DateTime.Now
            };

            _repo.Create(log);
        }

        public void LogTrace(string message) => CreateLog(0, message);
        public void LogDebug(string message) => CreateLog(1, message);
        public void LogInfo(string message) => CreateLog(2, message);
        public void LogWarning(string message) => CreateLog(3, message);
        public void LogError(string message) => CreateLog(4, message);
        public void LogCritical(string message) => CreateLog(5, message);

    }
}
