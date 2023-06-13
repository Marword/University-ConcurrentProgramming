namespace BallSimulator.Data.LoggerFiles;

public interface ILogWriter : IDisposable
{
    Task WriteAsync(IEnumerable<LogEntry> logEntries);
    Task WriteAsync(LogEntry logEntry);
}