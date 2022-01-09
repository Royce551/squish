namespace Squish.Services;

public enum Severity
{
    Debug,
    Info,
    Warning,
    Error,
    Fatal
}

public class LoggingService
{
    public static void Log(string text, Severity severity)
    {
        Console.ForegroundColor = severity switch
        {
            Severity.Debug or Severity.Info => ConsoleColor.White,
            Severity.Warning => ConsoleColor.Yellow,
            Severity.Error => ConsoleColor.Red,
            Severity.Fatal => ConsoleColor.Magenta,
            _ => ConsoleColor.White
        };
        var prefix = severity switch
        {
            Severity.Info => "Infor",
            Severity.Debug => "Debug",
            Severity.Warning => "Warni",
            Severity.Error => "Error",
            Severity.Fatal or _ => "Fatal"
        };
        Console.WriteLine($"{prefix}: {text}");
        Console.ResetColor();
    }

    public static void LogDebug(string text) => Log(text, Severity.Debug);
    public static void LogInfo(string text) => Log(text, Severity.Info);
    public static void LogWarning(string text) => Log(text, Severity.Warning);
    public static void LogError(string text) => Log(text, Severity.Error);
    public static void LogFatal(string text) => Log(text, Severity.Fatal);
}
