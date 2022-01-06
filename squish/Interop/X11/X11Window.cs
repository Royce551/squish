using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X11;
using static X11.Xlib;
using static Squish.Interop.X11.XlibExtensions;
using System.Runtime.InteropServices;
using Squish.Services;

namespace Squish.Interop.X11
{
    public unsafe class X11Window : IWindow
    {
        public string Title { get; }

        public byte[]? Icon { get; }

        public bool IsFocused
        {
            get
            {
                LoggingService.LogDebug($"is {Title} focused?");
                var focusedWindowId = (ulong[])X11Utilities.GetWindowProperty("_NET_ACTIVE_WINDOW", display, rootWindow);
                LoggingService.LogDebug($"{string.Join(", ", focusedWindowId)}");
                LoggingService.LogDebug($"{window == (Window)focusedWindowId[0]}");
                return window == (Window)focusedWindowId[0];
            }
            set
            {
                if (value)
                {
                    LoggingService.LogDebug("a");
                    var prop = XInternAtom(display, "_NET_ACTIVE_WINDOW", false);
                    var rootWindow = XRootWindow(display, 0);
                    var e = new XClientMessageEvent();
                    e.window = window;
                    e.message_type = prop;
                    e.format = 32;
                    LoggingService.LogDebug("a");
                    var time = (ulong[])X11Utilities.GetWindowProperty("_NET_WM_USER_TIME", display, window);
                    LoggingService.LogDebug($"{time[0]}");
                    var data = new ulong[]
                    {
                        1, // source indication
                        time[0], //timestamp
                        (ulong)window,
                    };
                    LoggingService.LogDebug("about to alloc");
                    IntPtr dataPtr = Marshal.AllocHGlobal(Marshal.SizeOf(data));
                    Marshal.StructureToPtr(data, dataPtr, false);
                    LoggingService.LogDebug($"");
                    e.data = dataPtr;
                    LoggingService.LogDebug("a");
                    IntPtr eventPtr = Marshal.AllocHGlobal(Marshal.SizeOf(e));
                    Marshal.StructureToPtr(e, eventPtr, false);
                    LoggingService.LogDebug("a");
                    XSendEvent(display, rootWindow, false, (long)(EventMask.SubstructureRedirectMask | EventMask.SubstructureNotifyMask), eventPtr);
                }
                else
                {

                }
            }
        }

        private IntPtr display;

        private Window window;
        private Window rootWindow;

        public X11Window(IntPtr display, ulong windowId, Window rootWindow)
        {
            this.display = display;

            window = (Window)windowId;
            string name_return = "";
            XFetchName(display, window, ref name_return);
            Title = name_return; // TODO: XFree name_return
        }
    }
}
