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

    public IWindow ActiveWindow
    {
        get => RunningWindows.First(x => x.IsFocused == true);
        set
        {
            if (value is not null) FocusWindowCommand(value);
        }
    }

    public TaskbarViewModel()
    {
        App.WindowManager.WindowsUpdated += WindowManager_WindowsUpdated;
        Update();
    }

    private void WindowManager_WindowsUpdated(object? sender, EventArgs e) => Update();

    public void FocusWindowCommand(IWindow window) => window.IsFocused = true;

    public void Update()
    {
        RunningWindows = new(App.WindowManager.RunningWindows);
        this.RaisePropertyChanged(nameof(RunningWindows));
        this.RaisePropertyChanged(nameof(ActiveWindow));
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
