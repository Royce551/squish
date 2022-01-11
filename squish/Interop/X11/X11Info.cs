using TerraFX.Interop.Xlib;
using static TerraFX.Interop.Xlib.Xlib;
namespace Squish.Interop.X11;

public static class X11Info
{
    public static unsafe Display* Display { get; }

    public static unsafe Window DefaultRootWindow => XDefaultRootWindow(Display);

    public static unsafe int DefaultScreen => XDefaultScreen(Display);

    static unsafe X11Info()
    {
        Display = XOpenDisplay(null);
    }
}