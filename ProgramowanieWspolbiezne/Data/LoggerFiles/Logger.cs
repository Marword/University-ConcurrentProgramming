using BallSimulator.Data.API;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace BallSimulator.Data.LoggerFiles;

public class Logger : ILogger, IDisposable
{

    private readonly ILogWriter _logWriter;
    private readonly BlockingCollection<LogEntry> _logQueue = new();

    private Task? _writingAction;

    public Logger(string fileName = "")
        : this(new LogFileWriter(fileName))
    { }

    public Logger(ILogWriter logWriter)
    {
        _logWriter = logWriter;

        Start();
    }

    public void LogInfo(string message) => Log(message, LogLevel.Info);
    public void LogWarning(string message) => Log(message, LogLevel.Warning);
    public void LogError(string message) => Log(message, LogLevel.Error);

    private void Start()
    {
        _writingAction = Task.Run(WriteLoop);
    }

    private void Stop()
    {

        _logQueue.CompleteAdding();
        _writingAction?.Wait();
    }

    private void Log(string message, LogLevel level)
    {
        string timestamp = GetTimestamp().ToString("yyyy-MM-dd HH:mm:ss");
        _logQueue.Add(new LogEntry(level, message, timestamp));
    }

    private DateTime GetTimestamp()
    {
        return DateTime.Now;
    }

    private async void WriteLoop()
    {
        var logsEnumerable = _logQueue.GetConsumingEnumerable();
        try
        {
            foreach (var logEntry in logsEnumerable)
            {
                await _logWriter.WriteAsync(logEntry);
            }
        }
        catch (ObjectDisposedException)
        { }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);

        Stop();
        _writingAction?.Dispose();
        _writingAction = null;
        _logWriter.Dispose();
        _logQueue.Dispose();
    }
}