using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static X11.Xlib;
using static Squish.Interop.X11.XlibExtensions;
using X11;
using Squish.Services;

namespace Squish.Interop.X11
{
    public unsafe class X11Utilities
    {
        /// <summary>
        /// Gets a property from the specified window
        /// </summary>
        /// <param name="property">The property</param>
        /// <param name="display">The connection to the X server</param>
        /// <param name="window">The window</param>
        /// <returns>A string, short[], or ulong[] depending on the returned format</returns>
        public static object GetWindowProperty(string property, IntPtr display, Window window)
        {
            var prop = XInternAtom(display, property, false);
            XGetWindowProperty(display, window, prop, 0, ~0, false, 0 /*AnyPropertyType*/, out Atom actualTypeReturn, out int actualFormatReturn, out IntPtr nItemsReturn, out IntPtr bytesAfterReturn, out IntPtr propReturn);

            if (actualFormatReturn == 8)
            {
                LoggingService.LogDebug($"{property} is a string");
                var dataPtr = (UIntPtr*)propReturn.ToPointer(); // TODO: this needs to be XFree'd
                var data = new List<char>();
                for (var x = 0; x < nItemsReturn.ToInt32(); x++)
                {
                    data.Add((char)dataPtr[x]);
                } // TODO: figure out a better way to do this
                return new string(data.ToArray());
            }
            else if (actualFormatReturn == 16)
            {
                LoggingService.LogDebug($"{property} is a short[]");
                var dataPtr = (UIntPtr*)propReturn.ToPointer(); // TODO: this needs to be XFree'd

                var data = new List<short>();
                for (var x = 0; x < nItemsReturn.ToInt32(); x++)
                {
                    data.Add((short)dataPtr[x]);
                } // TODO: figure out a better way to do this

                return data.ToArray();
            }
            else if (actualFormatReturn == 32)
            {
                LoggingService.LogDebug($"{property} is a ulong[]");
                var dataPtr = (UIntPtr*)propReturn.ToPointer(); // TODO: this needs to be XFree'd

                var data = new List<ulong>();
                for (var x = 0; x < nItemsReturn.ToInt32(); x++)
                {
                    data.Add((ulong)dataPtr[x]);
                } // TODO: figure out a better way to do this

                return data.ToArray();
            }
            else throw new Exception($"Unexpected format: {actualFormatReturn}");
        }

    }
}
