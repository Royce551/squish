using static TerraFX.Interop.Xlib.Xlib;
using TerraFX.Interop.Xlib;
using Squish.Interop.Linux;

namespace Squish.Interop.X11;

public unsafe class X11Environment : LinuxEnvironment
{
    private readonly Thread eventLoopThread;
    public X11Environment()
    {
        var dpy = X11Info.Display;
        eventLoopThread = new Thread(() =>
        {
            while (!false && true)
            {
                XEvent nextEvent;
                XNextEvent(X11Info.Display, &nextEvent);
                switch (nextEvent.type)
                {
                    case PropertyNotify:
                        X11PropertyNotifyReceived?.Invoke(null, nextEvent.xproperty);
                        break;
                }
            }
        });

        eventLoopThread.Start();

        RunningWindows = new List<IWindow>();

        XSelectInput(X11Info.Display, X11Info.DefaultRootWindow, PropertyChangeMask);

        X11PropertyNotifyReceived += OnX11PropertyNotifyReceived;
        UpdateClientList();
    }

    private void OnX11PropertyNotifyReceived(object? sender, XPropertyEvent e)
    {
        //We're listening for events on the root window
        if (e.window != X11Info.DefaultRootWindow) return;
        
        switch (new string(XGetAtomName(X11Info.Display, e.atom)))
        {
            case "_NET_CLIENT_LIST":
                UpdateClientList();
                break;
        }
    }

    private void UpdateClientList()
    {
        var clientList = X11Utilities.GetWindowProperty<ulong>("_NET_CLIENT_LIST", X11Info.DefaultRootWindow, XA_WINDOW);

        //Find out which windows no longer exist
        var windowsToRemove = RunningWindows.Where(window => !clientList.Contains((ulong) (Window) window.WindowHandle)).ToList();
        foreach (var window in windowsToRemove)
        {
            WindowClosed?.Invoke(this, window);
            RunningWindows.Remove(window);
        }
        
        //Find out which windows to add
        var windowsToAdd = clientList.Where(windowHandle => RunningWindows.All(window => (ulong) (Window) window.WindowHandle != windowHandle))
            .Select(windowHandle => new X11Window((Window) windowHandle))
            .Cast<IWindow>()
            .ToList();

        foreach (var window in windowsToAdd)
        {
            RunningWindows.Add(window);
            WindowOpened?.Invoke(this, window);
        }
    }

    public static event EventHandler<XPropertyEvent>? X11PropertyNotifyReceived;

    public override IWindow? GetWindowForWindowHandle(IntPtr handle)
    {
        return RunningWindows.FirstOrDefault(window => (Window) window.WindowHandle == (Window) handle) ?? new X11Window((Window) handle);
    }

    public override event EventHandler<IWindow>? WindowOpened;
    public override event EventHandler<IWindow>? WindowClosed;
    
    public override List<IWindow> RunningWindows { get; }

    public override IWindow? FocusedWindow
    {
        get
        {
            var activeWindow = X11Utilities.GetWindowProperty<Window>("_NET_ACTIVE_WINDOW", X11Info.DefaultRootWindow, XA_WINDOW);
            return activeWindow.ItemCount == 0 ? null : RunningWindows.Find(window => (Window) window.WindowHandle == activeWindow[0]);
        }

        set => X11Utilities.SendMessageToRootWindow("_NET_ACTIVE_WINDOW", (Window) (value?.WindowHandle ?? Window.NULL), 2, CurrentTime);
    }
}
