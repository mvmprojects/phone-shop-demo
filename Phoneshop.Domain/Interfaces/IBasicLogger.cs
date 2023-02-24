namespace Phoneshop.Domain.Interfaces
{
    public interface IBasicLogger
    {
        void LogDebug(string message);
        void LogError(string message);
        void LogInfo(string message);
        void LogWarning(string message);
    }
}
