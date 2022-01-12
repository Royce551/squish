using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Xlib;
using static TerraFX.Interop.Xlib.Xlib;

namespace Squish.Interop.X11;

public unsafe sealed class X11Exception : Exception
{
    private static int lastErrorCode = -1;
    private static delegate*unmanaged<Display*, XErrorEvent*, int> oldHandler;
    
    [UnmanagedCallersOnly]
    private static int HandleXError(Display* display, XErrorEvent* evt)
    {
        lastErrorCode = evt->error_code;
        oldHandler(display, evt);
        return 0;
    }
    
    public static void InitialiseExceptionHandling()
    {
        oldHandler = XSetErrorHandler(&HandleXError);
    }
    
    public static void ThrowForErrorCode(int errorCode)
    {
        if (errorCode != Success)
        {
            XSync(X11Info.Display, 0);
            
            if (lastErrorCode != -1) errorCode = lastErrorCode;
            
            const int BufferLength = 256;
            sbyte* str = stackalloc sbyte[BufferLength];
            var result = XGetErrorText(X11Info.Display, errorCode, str, BufferLength);

            if (result == Success)
            {
                throw new X11Exception(errorCode, new string(str));
            }
            else
            {
                // Couldn't get an error string; include the error code and hope it's useful
                throw new X11Exception(errorCode);
            }
        }
    }

    public int ErrorCode { get; }

    public X11Exception() { }

    public X11Exception(int errorCode) => ErrorCode = errorCode;

    public X11Exception(string? message) : base(message) { }

    public X11Exception(int errorCode, string? message) : base(message) => ErrorCode = errorCode;

    public X11Exception(string? message, Exception innerException) : base(message, innerException) { }
}
