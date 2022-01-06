using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Squish.Models;
using Squish.ViewModels.Taskbar;

namespace Squish.Views.Taskbar
{
    public partial class Taskbar : Window
    {
        private TaskbarViewModel ViewModel => DataContext as TaskbarViewModel ?? throw new System.Exception();

        public Taskbar()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void Background_PointerPressed(object sender, PointerPressedEventArgs e) => BeginMoveDrag(e);

        //private void TaskbarItem_PointerPressed(object sender, PointerPressedEventArgs e)
        //{
        //    var cmd = (Grid)sender;
        //    if (cmd.DataContext is TaskbarWindow x)
        //    {
        //        ViewModel.FocusWindowCommand(x.Id);
        //    }
        //}
    }
}
