﻿namespace Squish.Interop;

public interface IWindowManager
{
    public List<IWindow> RunningWindows { get; }

    public IWindow? FocusedWindow { get; set; }

    public IWindow? WindowForWindowHandle(IntPtr handle);
    
    public event EventHandler<IWindow>? WindowOpened;
    public event EventHandler<IWindow>? WindowClosed;
}