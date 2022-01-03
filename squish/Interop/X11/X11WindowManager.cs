using Squish.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X11;

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
            display = Xlib.XOpenDisplay(null);
            if (display == IntPtr.Zero) throw new Exception("XOpenDisplay failed");

            defaultScreen = Xlib.XDefaultScreen(display);

            rootWindow = Xlib.XRootWindow(display, defaultScreen);
            defaultRootWindow = Xlib.XDefaultRootWindow(display);
            
        }

        public void FocusWindow(string id)
        {
            throw new NotImplementedException();
        }
    }
}
