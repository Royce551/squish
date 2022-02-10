using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Squish.Interop;
using Squish.Services;

namespace Squish.Views.Desktop
{
    public partial class Desktop : Window
    {
        public Desktop()
        {
            LoggingService.LogDebug("Starting desktop");
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        protected override void OnPropertyChanged<T>(AvaloniaPropertyChangedEventArgs<T> change)
        {
            base.OnPropertyChanged(change);

            if (change.Property == ClientSizeProperty)
            {
                UpdateStruts();
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.X) UpdateStruts();
            base.OnKeyDown(e);
        }

        private void UpdateStruts()
        {
            LoggingService.LogDebug("UpdateStruts");
            if (Screens is null) return;

            var screenBounds = Screens.Primary.Bounds;

            var unioned = Screens.All.Aggregate(Screens.Primary.Bounds, (current, screen) => current.Union(screen.Bounds));

            var platformWindow = App.WindowManager.GetWindowForWindowHandle(PlatformImpl.Handle.Handle);
            if (platformWindow != null)
            {
                //TODO: This might smash in multi monitor setups
                platformWindow.WindowType = SystemWindowType.Desktop;
            }

            Position = new PixelPoint(0,0);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
