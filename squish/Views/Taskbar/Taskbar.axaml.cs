using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;

namespace Squish.Views.Taskbar
{
    public partial class Taskbar : Window
    {
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
    }
}
