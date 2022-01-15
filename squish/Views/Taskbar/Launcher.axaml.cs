using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Squish.Views.Taskbar
{
    public partial class Launcher : Window
    {
        public Launcher()
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
    }
}
