using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Squish.Interop;
using Squish.Models;
using Squish.Interop.X11;
using Squish.Services;
using Squish.Views.Taskbar;

namespace Squish;

public class App : Application
{
    public static readonly WindowManagementService Windows = new();

    public static IEnvironment WindowManager => Windows.WindowManager;

    public static ConfigurationFile Config => Program.Config;

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

    private void Desktop_Exit(object? sender, ControlledApplicationLifetimeExitEventArgs e)
    {

    }

    private void Desktop_Startup(object? sender, ControlledApplicationLifetimeStartupEventArgs e)
    {
        X11Exception.InitialiseExceptionHandling();

        LoggingService.Log("Starting desktop...", Severity.Info);
        new Taskbar().Show();
    }
}
