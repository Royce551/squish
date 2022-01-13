using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squish.Interop.Mock;

public class MockWindow : IWindow
{
    public object WindowHandle => null;

    public string Title => "i love catgirls";

    public Bitmap? Icon => null;

    public IWindow.ScreenMargins ReservedScreenArea { set { /*ignored*/} }
    public IWindow.SystemWindowType WindowType { set {/*ignored*/} }
}

