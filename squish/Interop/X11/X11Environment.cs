using Squish.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text;
using System.Threading.Tasks;
using TerraFX.Interop;
using static TerraFX.Interop.Xlib.Xlib;
using Squish.Services;
using TerraFX.Interop.Xlib;

namespace Squish.Interop.X11
{
    public unsafe static class X11Environment
    {/*
        public List<IWindow> RunningWindows
        {
            get
            {
                var squishRunningWindows = new List<IWindow>();
                var windows = (ulong[])X11Utilities.GetWindowProperty("_NET_CLIENT_LIST", display, &rootWindow);

                foreach (var windowId in windows)
                {
                    squishRunningWindows.Add(new X11Window(display, (nuint)windowId, rootWindow));
                }

                return squishRunningWindows;
            }
        }*/

        public static Display* Display { get; }
        public static Window DefaultRootWindow { get; }
        public static int DefaultScreen { get; }
        

        private Window rootWindow;

        private static Thread eventLoopThread;

        static X11WindowManager()
        {
            Display = XOpenDisplay(null);

            defaultScreen = XDefaultScreen(Display);

            rootWindow = XRootWindow(Display, defaultScreen);
            DefaultRootWindow = XDefaultRootWindow(Display);

            eventLoopThread = new Thread(() => 
            {
                while (true)
                {
                    XEvent nextEvent;
                    XNextEvent(Display, &nextEvent);

                    switch (nextEvent.type) {
                        case PropertyNotify:
                            X11PropertyNotifyEventReceived?.Invoke(null, nextEvent.xproperty);
                            break;
                    }

                }
            });

            eventLoopThread.Start();
        }

        public static event EventHandler<XPropertyEvent>? X11PropertyNotifyReceived;
   }
}
