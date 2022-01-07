using Avalonia.Controls;
using Squish.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squish.Interop
{
    public interface IWindowManager
    {
        public List<IWindow> RunningWindows { get; }

        public event EventHandler WindowsUpdated;
    }
}
