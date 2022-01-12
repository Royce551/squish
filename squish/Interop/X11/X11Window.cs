using System.Buffers.Binary;
using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using TerraFX.Interop.Xlib;
using Tmds.DBus;
using static TerraFX.Interop.Xlib.Xlib;

namespace Squish.Interop.X11;

public unsafe class X11Window : IWindow
{
    Window window;
    public object WindowHandle => window;

    public string Title
    {
        get
        {
            try
            {
                return X11Utilities.GetWindowProperty<sbyte>("_NET_WM_NAME", window, "UTF8_STRING".AsAtom()).AsString();
            }
            catch (X11Exception e)
            {
                return "";
            }
        }
    }

    public Bitmap? Icon
    {
        get
        {
            var xicon = X11Utilities.GetWindowProperty<nuint>("_NET_WM_ICON", window, XA_CARDINAL);
            if (xicon.ItemCount == 0) return new Bitmap("/usr/share/icons/hicolor/128x128/apps/microsoft-edge.png");

            nuint width = 0, height = 0;
            nuint* pIdealIcon = null;

            for (nuint* pData = xicon.Data; pData < xicon.Data + xicon.ItemCount;)
            {
                var (thisWidth, thisHeight) = (pData[0], pData[1]);
                if (thisWidth * thisHeight > width * height)
                {
                    (width, height) = (thisWidth, thisHeight);
                    pIdealIcon = pData;
                }
                pData += thisWidth * thisHeight + 2;
            }
            
            // int width = (int)xicon[0];
            // int height = (int)xicon[1];
            
            var bmp = new WriteableBitmap(new PixelSize((int)width, (int)height), new Vector(96, 96),
                PixelFormat.Bgra8888, AlphaFormat.Unpremul);

            using var framebuffer = bmp.Lock();

            for (var i = 0; i < (int)(width * height); i++)
            {
                var xData = pIdealIcon + i + 2;
                ((int*)framebuffer.Address)[i] = (int)*xData;
            }

            return bmp;
        }
    }

    public IWindow.ScreenMargins ReservedScreenArea
    {
        set
        {
            var struts = stackalloc long[12]
            {
                value.Left,
                value.Right,
                value.Top,
                value.Bottom,
                value.LeftStart,
                value.LeftEnd,
                value.RightStart,
                value.RightEnd,
                value.TopStart,
                value.TopEnd,
                value.BottomStart,
                value.BottomEnd
            };

            XChangeProperty(X11Info.Display, window, X11Utilities.XUInternAtom("_NET_WM_STRUT_PARTIAL"), XA_CARDINAL,
                32, PropModeReplace, (byte*) struts, 12);
        }
    }

    public IWindow.SystemWindowType WindowType
    {
        set
        {
            long desktop = 0xFFFFFFFF;
            XChangeProperty(X11Info.Display, window, X11Utilities.XUInternAtom("_NET_WM_DESKTOP"), XA_CARDINAL, 32,
                PropModeReplace, (byte*) &desktop, 1);
            
            var useSecondAtom = false;
            var atoms = stackalloc Atom[2];
            
            switch (value)
            {
                case IWindow.SystemWindowType.SkipTaskbarOnly:
                    //Set to _NET_WM_WINDOW_TYPE_NORMAL
                    atoms[0] = X11Utilities.XUInternAtom("_NET_WM_WINDOW_TYPE_NORMAL");
                    break;
                case IWindow.SystemWindowType.Desktop:
                    atoms[0] = X11Utilities.XUInternAtom("_NET_WM_WINDOW_TYPE_DESKTOP");
                    break;
                case IWindow.SystemWindowType.Taskbar:
                    atoms[0] = X11Utilities.XUInternAtom("_NET_WM_WINDOW_TYPE_DOCK");
                    break;
                case IWindow.SystemWindowType.Notification:
                    atoms[0] = X11Utilities.XUInternAtom("_NET_WM_WINDOW_TYPE_NOTIFICATION");
                    atoms[1] = X11Utilities.XUInternAtom("_KDE_NET_WM_WINDOW_TYPE_ON_SCREEN_DISPLAY");
                    useSecondAtom = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }

            XChangeProperty(X11Info.Display, window, X11Utilities.XUInternAtom("_NET_WM_WINDOW_TYPE"), XA_CARDINAL,
                32, PropModeReplace, (byte*) atoms, useSecondAtom ? 2 : 1);

        }
    }

    public X11Window(Window windowId)
    {
        window = windowId;
        
        XWindowAttributes attrs;
        XGetWindowAttributes(X11Info.Display, windowId, &attrs);
        XSelectInput(X11Info.Display, windowId, attrs.your_event_mask | PropertyChangeMask | StructureNotifyMask | SubstructureNotifyMask);

        X11Environment.X11PropertyNotifyReceived += (_, xevent) =>
        {
            if (xevent.window != window) return;

            switch (new string(XGetAtomName(X11Info.Display, xevent.atom)))
            {
                case "_NET_WM_NAME":
                    TitleChanged?.Invoke(this, Title);
                    break;
                case "_NET_WM_ICON":
                    IconChanged?.Invoke(this, Icon);
                    break;
            }
        };
    }

    public event EventHandler<string>? TitleChanged;
    public event EventHandler<Bitmap>? IconChanged;
}
