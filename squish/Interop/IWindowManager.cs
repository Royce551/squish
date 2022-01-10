namespace Squish.Interop;

public interface IWindowManager
{
    public List<IWindow> RunningWindows { get; }

    public event EventHandler WindowsUpdated;
}
