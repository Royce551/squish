using TerraFX.Interop.Xlib;
using static TerraFX.Interop.Xlib.Xlib;
namespace Squish.Interop.X11;

public static class X11Info
{
    /// <summary>
    /// The display connection to be used for looking for events
    /// Do not use for handling events or the entire app will hang!
    /// </summary>
    public static unsafe Display* EventDisplay { get; }

    /// <summary>
    /// The display connection to be used for handling events
    /// Do not use for looking for events or the entire app will hang!
    /// </summary>
    public static unsafe Display* ActionDisplay { get; }

    public static unsafe Window DefaultRootWindow => XDefaultRootWindow(ActionDisplay);

    public static unsafe int DefaultScreen => XDefaultScreen(ActionDisplay);

    static unsafe X11Info()
    {
        EventDisplay = XOpenDisplay(null);
        ActionDisplay = XOpenDisplay(null);
    }
}