namespace Squish.Interop;

public interface IEnvironment
{
    /// <summary>
    /// All running windows in the environment
    /// </summary>
    public List<IWindow> RunningWindows { get; }

    /// <summary>
    /// Fired when windows are added or removed, or when window data changes
    /// </summary>
    public event EventHandler WindowsUpdated;
}
