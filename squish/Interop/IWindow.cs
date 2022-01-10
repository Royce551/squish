namespace Squish.Interop;

public interface IWindow
{
    /// <summary>
    /// Title of the window - to be seen on the taskbar
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// Data for the icon of the app in some sort of format Avalonia can display
    /// </summary>
    public byte[]? Icon { get; }

    /// <summary>
    /// Whether the window is focused
    /// </summary>
    public bool IsFocused { get; set; }
}
