using static TerraFX.Interop.Xlib.Xlib;

namespace Squish.Interop.X11;

public unsafe sealed class X11Exception : Exception
{
    public static void ThrowForErrorCode(int errorCode)
    {
        if (errorCode != Success)
        {
            if (App.WindowManager is not X11Environment x11Environment)
                throw new X11Exception("Cannot be used while not using X11 backend");

            const int BufferLength = 256;
            sbyte* str = stackalloc sbyte[BufferLength];
            var result = XGetErrorText(x11Environment.Display, errorCode, str, BufferLength);

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
