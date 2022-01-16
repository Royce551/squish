using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Squish.Interop;
using Squish.Services;

namespace Squish.Views.Widgetbar
{
    public partial class Widgetbar : Window
    {
        public Widgetbar()
        {
            LoggingService.LogDebug("Starting widgetbar");
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        protected override void OnPropertyChanged<T>(AvaloniaPropertyChangedEventArgs<T> change)
        {
            base.OnPropertyChanged(change);

            if (change.Property == ClientSizeProperty)
            {
                UpdateStruts();
            }
        }

        private void UpdateStruts()
        {
            if (Screens is null) return;

            var screenBounds = Screens.Primary.Bounds;

            var unioned = Screens.All.Aggregate(Screens.Primary.Bounds, (current, screen) => current.Union(screen.Bounds));

            var platformWindow = App.WindowManager.GetWindowForWindowHandle(PlatformImpl.Handle.Handle);
            if (platformWindow != null)
            {
                //TODO: This might smash in multi monitor setups
                platformWindow.ReservedScreenArea = new IWindow.ScreenMargins(0, 0, (int)(unioned.X - screenBounds.X + Height), 0,
                    0, 0, 0, 0, screenBounds.X, (int)(screenBounds.X + Height), 0, 0);
                platformWindow.WindowType = IWindow.SystemWindowType.Taskbar;
            }

            Position = new PixelPoint(0,0);
        }
    }
}
