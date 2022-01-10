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
            LoggingService.Log("Using X11 environment", Severity.Info);
            WindowManager = new X11Environment();
            return;
        }
        throw new Exception("Platform not supported");
    }
}
