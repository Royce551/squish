using System.Text;
using TerraFX.Interop.Xlib;
using static TerraFX.Interop.Xlib.Xlib;
using System.Runtime.InteropServices;

namespace Squish.Interop.X11;

public static unsafe class X11Extensions
{
    public static string AsString(this WindowProperty<sbyte> windowProperty)
    {
        //TODO: Check if format = UTF8_STRING or XA_STRING
        //TODO: Convert the information in data to a string
        if (windowProperty.Type != XA_STRING || windowProperty.Type != AsAtom("UTF8_STRING"))
        {
            throw new ArgumentException("Not a string", nameof(windowProperty));
        }

        return new string(windowProperty.Data);
    }

    public static Atom AsAtom(this string str) => X11Utilities.XUInternAtom(str);

}

public unsafe class X11Utilities
{
    public static int ClientMesage { get; private set; }

    public static sbyte[] ToUtf8(string str)
    {
        int cb = Encoding.UTF8.GetMaxByteCount(str.Length);

        // Extra space for null byte
        // TODO: Perhaps use ArrayPool or something
        var data = new sbyte[cb + 1];
        Encoding.UTF8.GetBytes(str, MemoryMarshal.Cast<sbyte, byte>(data));
        return data;
    }

    public static Atom XUInternAtom(string atom, bool delete = false)
    {
        fixed (sbyte* pAtom = ToUtf8(atom))
        {
            return XInternAtom(X11Environment.Display, pAtom, 0);
        }
    }

    public static WindowProperty<T> GetWindowProperty<T>(string property, Window window, Atom type, nint offset = 0, nint length = int.MaxValue)
        where T : unmanaged
    {
        Atom typeReturn;
        int formatReturn;
        nuint nItems, bytesRemain;
        T* data;

        X11Exception.ThrowForErrorCode(
            XGetWindowProperty(X11Environment.Display,
                window,
                property.AsAtom(),
                offset,
                length,
                False,
                type,
                &typeReturn,
                &formatReturn,
                &nItems,
                &bytesRemain,
                (byte**)&data)
            );

        if (sizeof(T) * 8 != formatReturn)
            throw new X11Exception($"Wrong type attribute given, expected type which is {formatReturn} bits large (the format is {sizeof(T) * 8} bits of soze)");


        return new WindowProperty<T>(typeReturn, formatReturn, nItems, bytesRemain, data);
    }

    public static void SendMessageToRootWindow(string message, Window* window, nint data0, nint data1 = 0, nint data2 = 0, nint data3 = 0, nint data4 = 0)
    {
        var @event = new XEvent()
        {
            xclient =
                {
                    type = ClientMesage,
                    serial = 0,
                    send_event = True,
                    message_type = message.AsAtom(),
                    window = *window,
                    format = 32,
                    data =
                    {
                        l =
                        {
                            [0] = data0,
                            [1] = data1,
                            [2] = data2,
                            [3] = data3,
                            [4] = data4
                        }
                    }
                }
        };
        X11Exception.ThrowForErrorCode(
            XSendEvent(X11Environment.Display, *window, False, SubstructureRedirectMask | SubstructureNotifyMask, &@event));
    }
}
