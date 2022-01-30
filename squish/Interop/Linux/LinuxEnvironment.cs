using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squish.Interop.Linux;

public abstract class LinuxEnvironment : IEnvironment
{
    public abstract List<IWindow> RunningWindows { get; }
    public abstract IWindow? FocusedWindow { get; set; }

    public abstract event EventHandler<IWindow>? WindowOpened;
    public abstract event EventHandler<IWindow>? WindowClosed;

    public abstract IWindow? GetWindowForWindowHandle(IntPtr handle);

    public void Logout()
    {
        throw new NotImplementedException();
    }

    public void Restart()
    {
        throw new NotImplementedException();
    }

    public void Shutdown()
    {
        throw new NotImplementedException();
    }

    public void Sleep()
    {
        throw new NotImplementedException();
    }
}
