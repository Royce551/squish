using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using System.Collections.ObjectModel;
using System.Globalization;
using ReactiveUI;
using Squish.Interop;

namespace Squish.ViewModels.Taskbar;

public class TaskbarViewModel : ViewModelBase
{
    public ObservableCollection<IWindow> RunningWindows { get; set; } = new();

    public static IWindow? ActiveWindow
    {
        get => App.WindowManager.FocusedWindow;
        set => App.WindowManager.FocusedWindow = value;
    }

    public TaskbarViewModel()
    {
        App.WindowManager.WindowOpened += WindowManager_WindowsUpdated;
        App.WindowManager.WindowClosed += WindowManager_WindowsUpdated;
        Update();
    }

    private void WindowManager_WindowsUpdated(object? sender, IWindow e) => Update();

    public void Update()
    {
        RunningWindows = new ObservableCollection<IWindow>(App.WindowManager.RunningWindows);
        this.RaisePropertyChanged(nameof(RunningWindows));
        // this.RaisePropertyChanged(nameof(ActiveWindow));
        
        
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

public class BytesToImageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null) return new Bitmap("/usr/share/icons/hicolor/128x128/apps/microsoft-edge.png");
        if (value is Bitmap b)
        {
            return b;
        }
        if (value is byte[] x)
        {
            return new Bitmap(new MemoryStream(x));
        }
        else throw new Exception("baka");
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
