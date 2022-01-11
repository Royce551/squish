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
