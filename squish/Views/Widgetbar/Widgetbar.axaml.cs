using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Squish.Views.Widgetbar
{
    public partial class Widgetbar : Window
    {
        public Widgetbar()
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
