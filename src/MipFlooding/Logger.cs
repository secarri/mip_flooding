using System;
using System.IO;

public enum LogLevel
{
    Info,
    Warning,
    Error
}

public class Logger
{
    private readonly string _logFilePath;
    private readonly object _lock = new object();

    public Logger(string logFilePath)
    {
        _logFilePath = logFilePath;
        // Ensure the directory exists
        string directory = Path.GetDirectoryName(_logFilePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }

    private void Log(string message, LogLevel level)
    {
        string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} [{level}] {message}";

        lock (_lock)
        {
            // Append the log entry to the log file
            File.AppendAllText(_logFilePath, logEntry + Environment.NewLine);
        }
    }

    public void LogInfo(string message)
    {
        Log(message, LogLevel.Info);
    }

    public void LogWarning(string message)
    {
        Log(message, LogLevel.Warning);
    }

    public void LogError(string message)
    {
        Log(message, LogLevel.Error);
    }
}
