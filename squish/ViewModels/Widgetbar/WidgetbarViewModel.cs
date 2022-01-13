using Avalonia.Controls;
using Squish.Views.Widgetbar;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using ReactiveUI;

namespace Squish.ViewModels.Widgetbar;

public class WidgetbarViewModel : ViewModelBase
{
    public ObservableCollection<UserControl> Widgets { get; set; } = new() { new TimeWidget() }; // TODO: get from plugin system
}
