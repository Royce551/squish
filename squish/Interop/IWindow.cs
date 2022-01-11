namespace Squish.Interop;

public interface IWindow
{
    public Object WindowHandle { get; }
    
    public string Title { get; }

    public byte[]? Icon { get; }
}
