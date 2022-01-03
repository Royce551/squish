using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Squish.Interop;
using Squish.Services;
using Squish.ViewModels;
using Squish.Views;
using Squish.Views.Taskbar;

namespace Squish
{
    public class App : Application
    {
        public static WindowManagementService Windows = new WindowManagementService();

        public static IWindowManager WindowManager => Windows.WindowManager;

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
            LoggingService.Log("Starting desktop...", Severity.Info);
            new Taskbar().Show();
        }
    }
}
