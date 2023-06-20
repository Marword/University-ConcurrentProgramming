using BallSimulator.Data.LoggerFiles;

public class LogEntry
{
    public LogLevel Level { get; }
    public string Message { get; }
    public string Timestamp { get; }

    public LogEntry(LogLevel level, string message, string timestamp)
    {
        Level = level;
        Message = message;
        Timestamp = timestamp;
    }
}
