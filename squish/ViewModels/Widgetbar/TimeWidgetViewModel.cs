using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Avalonia.Threading;
using ReactiveUI;

namespace Squish.ViewModels.Widgetbar;

public class TimeWidgetViewModel : ViewModelBase
{
    public string Time => TimeOnly.FromDateTime(DateTime.Now).ToString();

    private System.Timers.Timer timer;
    public TimeWidgetViewModel()
    {
        timer = new System.Timers.Timer(100);
        timer.Elapsed += Timer_Elapsed;
        timer.Start();
    } // TODO: handle closing

    private async void Timer_Elapsed(object? sender, ElapsedEventArgs e) => await Dispatcher.UIThread.InvokeAsync(() => this.RaisePropertyChanged(nameof(Time)));
}
