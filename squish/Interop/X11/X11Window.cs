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
            var xicon = X11Utilities.GetWindowProperty<ulong>("_NET_WM_ICON", window, XA_CARDINAL);
            if (xicon.ItemCount == 0) return new Bitmap("/usr/share/icons/hicolor/128x128/apps/microsoft-edge.png");
            var bmp = new WriteableBitmap(new PixelSize((int) xicon[0], (int) xicon[1]), new Vector(96, 96),
                PixelFormat.Rgba8888, AlphaFormat.Unpremul);

            using var framebuffer = bmp.Lock();
            // for (int y = 0; y < (int) xicon[1]; y++)
            // {
            //     var scanLine = (ulong*) framebuffer.Address + y * (int) xicon[0];
            //     for (int x = 0; x < (int) xicon[0]; x++)
            //     {
            //         int pixel = y * (int) xicon[0] + x;
            //         Buffer.MemoryCopy(xicon.Data + pixel + 2, scanLine + x, 1, 1);
            //         // scanLine[x] = xicon[pixel + 2];
            //     }
            //     
            // }
            Buffer.MemoryCopy(xicon.Data + 2, (void*) framebuffer.Address,
                framebuffer.RowBytes * framebuffer.Size.Height, (int) xicon[0] * (int) xicon[1] * 4);

            return bmp;
        }
    }

    public IWindow.ScreenMargins ReservedScreenArea
    {
        set
        {
            var struts = Marshal.AllocHGlobal(sizeof(long) * 12);
            Marshal.WriteInt64(struts, value.Left);
            Marshal.WriteInt64(struts + 1 * 8, value.Right);
            Marshal.WriteInt64(struts + 2 * 8, value.Top);
            Marshal.WriteInt64(struts + 3 * 8, value.Bottom);
            Marshal.WriteInt64(struts + 4 * 8, value.LeftStart);
            Marshal.WriteInt64(struts + 5 * 8, value.LeftEnd);
            Marshal.WriteInt64(struts + 6 * 8, value.RightStart);
            Marshal.WriteInt64(struts + 7 * 8, value.RightEnd);
            Marshal.WriteInt64(struts + 8 * 8, value.TopStart);
            Marshal.WriteInt64(struts + 9 * 8, value.TopEnd);
            Marshal.WriteInt64(struts + 10 * 8, value.BottomStart);
            Marshal.WriteInt64(struts + 11 * 8, value.BottomEnd);

            XChangeProperty(X11Info.Display, window, X11Utilities.XUInternAtom("_NET_WM_STRUT_PARTIAL"), XA_CARDINAL,
                32, PropModeReplace, (byte*) struts, 12);
            
            Marshal.FreeHGlobal(struts);
            
            // X11Exception.ThrowForErrorCode(error);
        }
    }

    public IWindow.SystemWindowType WindowType
    {
        set
        {
            nuint atom, atom2 = 0;
            
            switch (value)
            {
                case IWindow.SystemWindowType.SkipTaskbarOnly:
                    //Set to _NET_WM_WINDOW_TYPE_NORMAL
                    atom = X11Utilities.XUInternAtom("_NET_WM_WINDOW_TYPE_NORMAL");
                    break;
                case IWindow.SystemWindowType.Desktop:
                    atom = X11Utilities.XUInternAtom("_NET_WM_WINDOW_TYPE_DESKTOP");
                    break;
                case IWindow.SystemWindowType.Taskbar:
                    atom = X11Utilities.XUInternAtom("_NET_WM_WINDOW_TYPE_DOCK");
                    break;
                case IWindow.SystemWindowType.Notification:
                    atom = X11Utilities.XUInternAtom("_NET_WM_WINDOW_TYPE_NOTIFICATION");
                    atom2 = X11Utilities.XUInternAtom("_KDE_NET_WM_WINDOW_TYPE_ON_SCREEN_DISPLAY");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }

            var atoms = Marshal.AllocHGlobal(sizeof(long) * 2);
            Marshal.WriteInt64(atoms, (long) atom);
            Marshal.WriteInt64(atoms + 8, (long) atom2);
            
            XChangeProperty(X11Info.Display, window, X11Utilities.XUInternAtom("_NET_WM_STRUT_PARTIAL"), XA_CARDINAL,
                32, PropModeReplace, (byte*) atoms, atom2 == 0 ? 1 : 2);

            Marshal.FreeHGlobal(atoms);
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
