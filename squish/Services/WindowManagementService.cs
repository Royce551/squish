using Squish.Interop;
using Squish.Interop.Linux;
using Squish.Interop.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squish.Services
{
    public class WindowManagementService
    {
        public IWindowManager WindowManager { get; private set; }

        public WindowManagementService()
        {
            if (OperatingSystem.IsLinux()) WindowManager = new LinuxWindowManager();
            else WindowManager = new MockWindowManager();
        }
    }
}
