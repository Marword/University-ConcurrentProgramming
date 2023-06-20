using System.Runtime.CompilerServices;

namespace BallSimulator.Data.API;

public interface ILogger : IDisposable
{
    public void LogInfo(string message);
    public void LogWarning(string message);
    public void LogError(string message);
}