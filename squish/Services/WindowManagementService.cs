using Squish.Interop;
using Squish.Interop.X11;
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
            LoggingService.Log("Starting window management service", Severity.Info);
            if (OperatingSystem.IsLinux())
            {
                LoggingService.Log("Using X11 window manager", Severity.Info);
                WindowManager = new X11WindowManager();
                return;
            }

            LoggingService.Log("No window manager found for platform, using mocks", Severity.Warning);
            WindowManager = new MockWindowManager();
        }
    }
}
