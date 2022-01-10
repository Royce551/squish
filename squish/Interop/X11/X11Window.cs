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

    public byte[]? Icon => throw new NotImplementedException();

    public bool IsFocused { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    private readonly Window window;
    private readonly X11Environment x11Environment;

    public X11Window(Window window, X11Environment environment)
    {
        this.window = window;
        this.x11Environment = environment;

        XWindowAttributes attrs;
        XGetWindowAttributes(x11Environment.Display, window, &attrs);
        XSelectInput(x11Environment.Display, window, attrs.your_event_mask | PropertyChangeMask | StructureNotifyMask | SubstructureNotifyMask);

        x11Environment.X11PropertyNotifyReceived += (_, xevent) =>
        {
            if (xevent.window != this.window) return;

            switch (new string(XGetAtomName(x11Environment.Display, xevent.atom)))
            {
                case "_NET_WM_NAME":
                    TitleChanged?.Invoke(this, this.Title);
                    break;
                case "_NET_WM_ICON":
                    IconChanged?.Invoke(this, null /* TODO: this.Icon */);
                    break;
            }
        };
    }

    public event EventHandler<string>? TitleChanged;
    public event EventHandler<X11Icon>? IconChanged;
}
