using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Squish.Interop;
using Squish.Models;
using Squish.Interop.X11;
using Squish.Services;
using Squish.Views.Taskbar;
using Squish.Views.Desktop;
using Squish.Views.Widgetbar;
using Avalonia.Markup.Xaml.Styling;
using Squish.Interop.Mock;

namespace Squish;

public class App : Application
{
    public static IEnvironment Environment { get; private set; }

    public static ConfigurationFile Config => Program.Config;

    static App()
    {
        LoggingService.LogInfo("Picking environment...");
        if (OperatingSystem.IsLinux())
        {
            Environment = new X11Environment();
            LoggingService.LogInfo("Using X11 environment");
        }
        else
        {
            Environment = new MockEnvironment();
            LoggingService.LogWarning("No environment found for environment, using mocks; most features will not be available");
        }
    }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IControlledApplicationLifetime desktop)
        {
            desktop.Startup += Desktop_Startup;
            desktop.Exit += Desktop_Exit;
        }
        //if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        //{
        //    desktop.ShutdownMode = Avalonia.Controls.ShutdownMode.OnLastWindowClose;

        //    desktop.MainWindow = new MainWindow
        //    {
        //        DataContext = new MainWindowViewModel(),
        //    };
        //}

        base.OnFrameworkInitializationCompleted();
    }

    private async void Desktop_Exit(object? sender, ControlledApplicationLifetimeExitEventArgs e)
    {
        await JsonService.WriteAsync(ConfigurationFile.SavePath, Config);
    }

    private void Desktop_Startup(object? sender, ControlledApplicationLifetimeStartupEventArgs e)
    {
        if (Environment is X11Environment)
            X11Exception.InitialiseExceptionHandling();

        Config.PropertyChanged += Config_PropertyChanged;
        HandleTheme();

        LoggingService.Log("Starting shell...", Severity.Info);
        new Desktop().Show();
        new Taskbar().Show();
        new Widgetbar().Show();
    }

    private void HandleTheme()
    {
        LoggingService.LogInfo("Theme is changing!");
        var lightSIADLTheme = new StyleInclude(new Uri("avares://SIADL.Avalonia"))
        {
            Source = new Uri("avares://SIADL.Avalonia/LightTheme.axaml")
        };
        //var lightFluentTheme = new StyleInclude(new Uri("avares://Squish"))
        //{
        //    Source = new Uri("resm:Avalonia.Themes.Default.Accents.BaseLight.xaml?assembly=Avalonia.Themes.Default")
        //};  i don't think these are actually needed but i'll leave these commented for now

        var darkSIADLTheme = new StyleInclude(new Uri("avares://SIADL.Avalonia"))
        {
            Source = new Uri("avares://SIADL.Avalonia/DarkTheme.axaml")
        };
        //var darkFluentTheme = new StyleInclude(new Uri("avares://Squish"))
        //{
        //    Source = new Uri("resm:Avalonia.Themes.Default.Accents.BaseDark.xaml?assembly=Avalonia.Themes.Default")
        //};

        switch (Config.Theme)
        {
            case Theme.Light:
                Styles[1] = lightSIADLTheme;
                break;
            case Theme.Dark:
                Styles[1] = darkSIADLTheme;
                break;
        }
    }

    private void Config_PropertyChanged(object? sender, string e)
    {
        LoggingService.LogDebug($"The configuration property {e} changed");
        if (e == nameof(Config.Theme)) HandleTheme();
    }
}
