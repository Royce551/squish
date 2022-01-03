using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squish.Services
{
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
    }
}
