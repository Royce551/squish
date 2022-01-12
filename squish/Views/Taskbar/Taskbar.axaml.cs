using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using Squish.Interop;
using Squish.ViewModels.Taskbar;

namespace Squish.Views.Taskbar;

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
            platformWindow.ReservedScreenArea = new IWindow.ScreenMargins(0, (int) (unioned.Right - screenBounds.Right + Width), 0, 0,
                0, 0, screenBounds.Y, screenBounds.Bottom, 0, 0, 0, 0);
            platformWindow.WindowType = IWindow.SystemWindowType.Taskbar;
        }
        
        Position = new PixelPoint((int) (screenBounds.Right - Width), screenBounds.Y); 
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    // private void Background_PointerPressed(object sender, PointerPressedEventArgs e) => BeginMoveDrag(e);

    //private void TaskbarItem_PointerPressed(object sender, PointerPressedEventArgs e)
    //{
    //    var cmd = (Grid)sender;
    //    if (cmd.DataContext is TaskbarWindow x)
    //    {
    //        ViewModel.FocusWindowCommand(x.Id);
    //    }
    //}
    protected override void OnOpened(EventArgs e)
    {
        base.OnOpened(e);

        UpdateStruts();

        var screenBounds = Screens.Primary.Bounds;
        Height = screenBounds.Height;
    }
}
