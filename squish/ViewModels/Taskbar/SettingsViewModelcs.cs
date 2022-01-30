using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using Squish.Models;

namespace Squish.ViewModels.Taskbar;

public class SettingsViewModel : ViewModelBase
{
    public int ThemeSelectedIndex
    {
        get => (int)App.Config.Theme;
        set
        {
            App.Config.Theme = (Theme)value;
            App.Config.RaisePropertyChanged(nameof(App.Config.Theme));
        }
    }
}
