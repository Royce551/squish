using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace Squish.Interop;

public interface IWindow
{
    public object WindowHandle { get; }
    
    public string Title { get; }

    public Bitmap? Icon { get; }

    public ScreenMargins ReservedScreenArea { set; }

    public SystemWindowType WindowType { set; }
}

public struct ScreenMargins
{
    public int Left;
    public int Right;
    public int Top;
    public int Bottom;
    public int LeftStart;
    public int LeftEnd;
    public int RightStart;
    public int RightEnd;
    public int TopStart;
    public int TopEnd;
    public int BottomStart;
    public int BottomEnd;

    public ScreenMargins(int left, int right, int top, int bottom, int leftStart, int leftEnd, int rightStart, int rightEnd, int topStart, int topEnd, int bottomStart, int bottomEnd)
    {
        Left = left;
        Right = right;
        Top = top;
        Bottom = bottom;
        LeftStart = leftStart;
        LeftEnd = leftEnd;
        RightStart = rightStart;
        RightEnd = rightEnd;
        TopStart = topStart;
        TopEnd = topEnd;
        BottomStart = bottomStart;
        BottomEnd = bottomEnd;
    }
}

public enum SystemWindowType
{
    SkipTaskbarOnly,
    Desktop,
    Taskbar,
    Notification
}
