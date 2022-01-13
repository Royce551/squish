﻿using Squish.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squish.Interop.Mock;

public class MockEnvironment : IEnvironment
{
    public List<IWindow> RunningWindows => new List<IWindow>() { new MockWindow() };

    public IWindow? FocusedWindow { get => RunningWindows[0]; set { /*ignored*/} }

    public event EventHandler<IWindow>? WindowOpened;
    public event EventHandler<IWindow>? WindowClosed;

    public IWindow? GetWindowForWindowHandle(IntPtr handle)
    {
        return RunningWindows[0];
    }
}