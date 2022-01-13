using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Squish.Views.Desktop
{
    public partial class Desktop : Window
    {
        public Desktop()
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
