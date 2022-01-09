using TerraFX.Interop.Xlib;
using static TerraFX.Interop.Xlib.Xlib;

namespace Squish.Interop.X11;

public unsafe class X11Window : IWindow
{
    public string Title
    {
        get
        {
            return X11Utilities.GetWindowProperty<sbyte>("_NET_WM_NAME", window, "UTF8_STRING".AsAtom()).AsString();
        }
    }

    Window window;

    public X11Window(Window windowId)
    {
        window = windowId;

        XWindowAttributes attrs;
        XGetWindowAttributes(X11Environment.Display, windowId, &attrs);
        XSelectInput(X11Environment.Display, windowId, attrs.your_event_mask | PropertyChangeMask | StructureNotifyMask | SubstructureNotifyMask);

        X11Environment.X11PropertyNotifyReceived += (_, xevent) =>
        {
            if (xevent.window != this.window) return;

            switch (new string(XGetAtomName(X11Environment.Display, xevent.atom)))
            {
                case "_NET_WM_NAME":
                    TitleChanged?.Invoke(this, this.Title);
                    break;
                case "_NET_WM_ICON":
                    IconChanged?.Invoke(this, null /* TODO: this.Icon */);
                    break;
            }
        }
        }

    public event EventHandler<string>? TitleChanged;
    public event EventHandler<X11Icon>? IconChanged;
}
