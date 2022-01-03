using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Squish.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squish.ViewModels.Taskbar
{
    public class TaskbarViewModel : ViewModelBase
    {
        public ObservableCollection<TaskbarWindow> RunningWindows { get; set; } = new();


        public TaskbarViewModel() => Update();

        public void Update()
        {
            RunningWindows = new(App.Windows.WindowManager.RunningWindows);
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
}
