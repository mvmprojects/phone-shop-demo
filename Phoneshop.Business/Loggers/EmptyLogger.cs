using Phoneshop.Domain.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace Phoneshop.Business
{
    [ExcludeFromCodeCoverage]
    public class EmptyLogger : IBasicLogger
    {
        public void LogDebug(string message) { }
        public void LogError(string message) { }
        public void LogInfo(string message) { }
        public void LogWarning(string message) { }
    }
}
