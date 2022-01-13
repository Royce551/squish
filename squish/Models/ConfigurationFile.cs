using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squish.Models;

public class ConfigurationFile
{
    /// <summary>
    /// The ISO language code for the current language, or "automatic" for the system default
    /// </summary>
    public string Language { get; init; } = "automatic";

    public string DesktopWallpaper { get; init; } = "Assets/placeholderDefaultBackground.png";

    public Stretch DesktopWallpaperStretchMode { get; init; } = Stretch.UniformToFill;
}
