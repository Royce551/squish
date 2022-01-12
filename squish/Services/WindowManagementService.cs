using Squish.Interop;
using Squish.Interop.X11;

namespace Squish.Services;

public class WindowManagementService
{
    public IEnvironment WindowManager { get; private set; }

    public WindowManagementService()
    {
        LoggingService.Log("Starting window management service", Severity.Info);
        if (OperatingSystem.IsLinux())
        {
            LoggingService.Log("Using X11 window manager", Severity.Info);
            WindowManager = new X11Environment();
            return;
        }
        throw new Exception("Platform not supported");
        //LoggingService.Log("No window manager found for platform, using mocks", Severity.Warning);
        //WindowManager = new MockWindowManager();
    }
}
