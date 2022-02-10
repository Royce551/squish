using Squish.Views.Taskbar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squish.ViewModels.Taskbar;

public class LauncherViewModel : ViewModelBase
{
    public void OpenSettingsCommand()
    {
        new Settings().Show();
    }

    public void OpenEndSessionMenuCommand()
    {
        App.Environment.LogoutPC();
    }
}