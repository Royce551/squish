using Squish.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squish.Interop.Mock;

public class MockEnvironment : IEnvironment
{
    public List<IWindow> RunningWindows => new List<IWindow>()
    {
        new MockWindow
        {
            Title = "I"
        },
        new MockWindow
        {
            Title = "love"
        },
        new MockWindow
        {
            Title = "cute"
        },
        new MockWindow
        {
            Title = "catgirls"
        }
    };

    public IWindow? FocusedWindow { get => RunningWindows[0]; set { /*ignored*/} }

    public event EventHandler<IWindow>? WindowOpened;
    public event EventHandler<IWindow>? WindowClosed;

    public IWindow? GetWindowForWindowHandle(IntPtr handle)
    {
        return RunningWindows[0];
    }

    public void LogoutPC()
    {
        throw new NotImplementedException();
    }

    public void RestartPC()
    {
        throw new NotImplementedException();
    }

    public void ShutdownPC()
    {
        throw new NotImplementedException();
    }

    public void SleepPC()
    {
        throw new NotImplementedException();
    }
}
