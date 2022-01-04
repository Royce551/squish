using Squish.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X11;
using static X11.Xlib;
using static Squish.Interop.X11.XlibExtensions;
using Squish.Services;

namespace Squish.Interop.X11
{
    public class X11WindowManager : IWindowManager
    {
        public List<TaskbarWindow> RunningWindows => throw new NotImplementedException();

        public event EventHandler WindowsUpdated;

        private IntPtr display;

        private int defaultScreen;

        private Window rootWindow;
        private Window defaultRootWindow;

        public X11WindowManager()
        {
            display = XOpenDisplay(null);
            if (display == IntPtr.Zero) throw new Exception("XOpenDisplay failed");

            LoggingService.LogDebug($"XOpenDisplay: display is now {display}");

            defaultScreen = XDefaultScreen(display);

            rootWindow = XRootWindow(display, defaultScreen);
            defaultRootWindow = XDefaultRootWindow(display);

            LoggingService.LogDebug($"XDefaultScreen - {defaultScreen}, XRootWindow - {rootWindow}, XDefaultRootWindow - {defaultRootWindow}");

            var prop = XInternAtom(display, "_NET_CLIENT_LIST", false);

            LoggingService.LogDebug($"XInternAtom: {prop}");

            XGetWindowProperty(display, rootWindow, prop, 0, ~0, false, (Atom)0 /*AnyPropertyType*/, out Atom actualTypeReturn, out int actualFormatReturn, out ulong nItemsReturn, out ulong bytesAfterReturn, out long[] propReturn);

            LoggingService.LogDebug($"XGetWindowProperty: actualTypeReturn: {actualTypeReturn}, actualFormatReturn: {actualFormatReturn}, nItemsReturn: {nItemsReturn}, bytesAfterReturn: {bytesAfterReturn}, propReturn: {string.Join(", ", propReturn)}");
        }

        public void FocusWindow(string id)
        {
            throw new NotImplementedException();
        }
    }
}
