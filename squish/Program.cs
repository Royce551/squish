using Avalonia;
using Avalonia.ReactiveUI;
using Squish.Models;
using Squish.Services;

namespace Squish;

class Program
{
    public static ConfigurationFile Config = default!; 
    //realistically this can't be null since it's set right at the start
    
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static async Task Main(string[] args)
    {
        var configPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "config.json");
        Config = await JsonService.ReadAsync<ConfigurationFile>(configPath);
        if (Config.Language != "automatic") Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Config.Language);

        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace()
            .UseReactiveUI();
}
