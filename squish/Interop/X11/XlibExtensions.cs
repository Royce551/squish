using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using X11;
using static X11.Xlib;

namespace Squish.Interop.X11
{
    public unsafe class XlibExtensions
    {
        [DllImport("libX11.so.6")]
        public static extern int XGetWindowProperty
        (
            IntPtr display,
            Window w,
            Atom property,
            long long_offset,
            long long_length,
            bool delete,
            Atom req_type,
            out Atom actual_type_return,
            out int actual_format_return,
            out IntPtr nitems_return,
            out IntPtr bytes_after_return,
            out IntPtr prop_return
        );
        
        [DllImport("libX11.so.6")]
        public static extern void XFree(string data);

        [DllImport("libX11.so.6")]
        public static extern Atom XInternAtom
        (
            IntPtr display,
            string atom_name,
            bool only_if_exists
        );
    }
}
