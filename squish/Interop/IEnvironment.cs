namespace Squish.Interop;

public interface IEnvironment
{
    public List<IWindow> RunningWindows { get; }

    public IWindow? FocusedWindow { get; set; }

    public IWindow? GetWindowForWindowHandle(IntPtr handle);
    
    public event EventHandler<IWindow>? WindowOpened;
    public event EventHandler<IWindow>? WindowClosed;

    public void ShutdownPC();

    public void RestartPC();

    public void LogoutPC();

    public void SleepPC();
}
