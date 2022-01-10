namespace Squish.Interop;

public interface IEnvironment
{
    public List<IWindow> RunningWindows { get; }

    public event EventHandler WindowsUpdated;
}
