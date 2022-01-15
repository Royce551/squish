using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using System.Collections.ObjectModel;
using System.Globalization;
using ReactiveUI;
using Squish.Interop;
using Squish.Views.Taskbar;

namespace Squish.ViewModels.Taskbar;

public class TaskbarViewModel : ViewModelBase
{
    public ObservableCollection<IWindow> RunningWindows { get; set; } = new(App.WindowManager.RunningWindows);

    public static IWindow? ActiveWindow
    {
        get => App.WindowManager.FocusedWindow;
        set => App.WindowManager.FocusedWindow = value;
    }

    public TaskbarViewModel()
    {
        App.WindowManager.WindowOpened += WindowManager_WindowOpened;
        App.WindowManager.WindowClosed += WindowManager_WindowClosed; // TODO: if the taskbar gets restarted this might leak memory
    }

    private void WindowManager_WindowClosed(object? sender, IWindow e) => RunningWindows.Remove(e);


    private void WindowManager_WindowOpened(object? sender, IWindow e) => RunningWindows.Add(e);

    public void OpenLauncherCommand()
    {
        new Launcher().Show();
    }
}

public class BoolToOpacityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool x)
        {
            if (x) return 1d;
            else return 0d;
        }
        else throw new Exception("baka");
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
