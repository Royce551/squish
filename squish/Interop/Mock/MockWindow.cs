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

    public string Title { get; set; } = "Window";

    public Bitmap? Icon => null;

    public ScreenMargins ReservedScreenArea { set { /*ignored*/} }
    public SystemWindowType WindowType { set {/*ignored*/} }
}

