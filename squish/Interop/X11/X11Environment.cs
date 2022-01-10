using static TerraFX.Interop.Xlib.Xlib;
using TerraFX.Interop.Xlib;

namespace Squish.Interop.X11;

public unsafe class X11Environment : IEnvironment
{
    public Display* Display { get; }
    public Window DefaultRootWindow { get; }
    public int DefaultScreen { get; }

    public List<IWindow> RunningWindows => throw new NotImplementedException();

    private Window rootWindow;

    private Thread eventLoopThread;

    public X11Environment()
    {
        Display = XOpenDisplay(null);

        DefaultScreen = XDefaultScreen(Display);

        rootWindow = XRootWindow(Display, DefaultScreen);
        DefaultRootWindow = XDefaultRootWindow(Display);

        eventLoopThread = new Thread(() =>
        {
            while (true)
            {
                XEvent nextEvent;
                XNextEvent(Display, &nextEvent);

                switch (nextEvent.type)
                {
                    case PropertyNotify:
                        X11PropertyNotifyReceived?.Invoke(null, nextEvent.xproperty);
                        break;
                }

            }
        });

        eventLoopThread.Start();
    }

    public event EventHandler<XPropertyEvent>? X11PropertyNotifyReceived;
    public event EventHandler? WindowsUpdated;
}
