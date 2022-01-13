using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using ReactiveUI;

namespace Squish.ViewModels.Desktop;

public class DesktopViewModel : ViewModelBase
{
    public Bitmap Background => new Bitmap(App.Config.DesktopWallpaper);

    public Stretch StretchMode => App.Config.DesktopWallpaperStretchMode;

    public void ChangeBackgroundCommand()
    {

    }
}
