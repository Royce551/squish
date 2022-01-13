using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Squish.Views.Widgetbar
{
    public partial class TimeWidget : UserControl
    {
        public TimeWidget()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
