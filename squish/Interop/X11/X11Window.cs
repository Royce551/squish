using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X11;
using static X11.Xlib;
using static Squish.Interop.X11.XlibExtensions;

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
                var prop = XInternAtom(display, "_NET_ACTIVE_WINDOW", false);
                XGetWindowProperty(display, window, prop, 0, ~0, false, 0, out Atom actualTypeReturn, out int actualFormatReturn, out IntPtr nItemsReturn, out IntPtr bytesAfterReturn, out IntPtr propReturn);
                var data = (UIntPtr*)propReturn.ToPointer();
                var focusedWindowId = (ulong)data[2];

                return window == (Window)focusedWindowId;
            }
            set
            {
                //if (value)
                //{
                //    var prop = XInternAtom(display, "_NET_ACTIVE_WINDOW", false);
                //    var rootWindow = XRootWindow(display, 0);
                //    XClientMessageEvent e;
                //    e.window = window;
                //    e.message_type = prop;
                //    e.format = 32;
                //    e.data

                //    var x = &e;
                    
                    

                //    //var userTimeProp = XInternAtom(display, "_NET_WM_USER_TIME", false);
                //    //XGetWindowProperty()


                //    var data = new ulong[]
                //    {
                //        1,

                //    };

                //    XSendEvent(display, 
                //}
                //else
                //{

                //}
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
