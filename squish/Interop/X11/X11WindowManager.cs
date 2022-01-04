using Squish.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X11;
using static X11.Xlib;
using static X11.Xutil;
using static X11.xcb;
using static X11.Xmu;
using static Squish.Interop.X11.XlibExtensions;
using Squish.Services;

namespace Squish.Interop.X11
{
    public class X11WindowManager : IWindowManager
    {
        public List<TaskbarWindow> RunningWindows
        {
            get
            {
                var squishRunningWindows = new List<TaskbarWindow>();
                unsafe
                {
                    var prop = XInternAtom(display, "_NET_CLIENT_LIST", false);
                    XGetWindowProperty(display, rootWindow, prop, 0, ~0, false, (Atom)0 /*AnyPropertyType*/, out Atom actualTypeReturn, out int actualFormatReturn, out IntPtr nItemsReturn, out IntPtr bytesAfterReturn, out IntPtr propReturn);
                    var data = (UIntPtr*)propReturn.ToPointer();

                    var windows = new List<ulong>();
                    for (var x = 0; x < nItemsReturn.ToInt32(); x++)
                    {
                        windows.Add((ulong)data[x]);
                    }

                    foreach (var windowId in windows)
                    {
                        var window = (Window)windowId;
                        string name_return = "";
                        XFetchName(display, window, ref name_return);
                        squishRunningWindows.Add(new TaskbarWindow
                        {
                            Id = windowId.ToString(),
                            Title = name_return
                        });
                    }

                    foreach (var window in squishRunningWindows)
                    {
                        LoggingService.LogDebug($"{window.Id} - {window.Title}, {window.IsActiveWindow}");
                    }

                    return squishRunningWindows;
                }
            }
        }

        public event EventHandler WindowsUpdated;

        private IntPtr display;

        private int defaultScreen;

        private Window rootWindow;
        private Window defaultRootWindow;

        public X11WindowManager()
        {
            display = XOpenDisplay(null);
            if (display == IntPtr.Zero) throw new Exception("XOpenDisplay failed");

            //LoggingService.LogDebug($"XOpenDisplay: display is now {display}");

            defaultScreen = XDefaultScreen(display);

            rootWindow = XRootWindow(display, defaultScreen);
            defaultRootWindow = XDefaultRootWindow(display);

            //LoggingService.LogDebug($"XDefaultScreen - {defaultScreen}, XRootWindow - {rootWindow}, XDefaultRootWindow - {defaultRootWindow}");

            //var prop = XInternAtom(display, "_NET_CLIENT_LIST", false);

            //LoggingService.LogDebug($"XInternAtom: {prop}");
            //unsafe
            //{
                
            //}
            
        }

        public void FocusWindow(string id)
        {
            throw new NotImplementedException();
        }
    }
}
