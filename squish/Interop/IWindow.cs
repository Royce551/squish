namespace Squish.Interop;

public interface IWindow
{
    public string Title { get; }

    public byte[]? Icon { get; }

    public bool IsFocused { get; set; }
}
