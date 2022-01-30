using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squish.Models;

public class ConfigurationFile
{
    public static string SavePath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "squish", "config.json");

    public event EventHandler<string>? PropertyChanged; // probably not the best system feel free to switch this with a better one

    public void RaisePropertyChanged(string property) => PropertyChanged?.Invoke(null, property);

    /// <summary>
    /// The ISO language code for the current language, or "automatic" for the system default
    /// </summary>
    public string Language { get; set; } = "automatic";

    public string DesktopWallpaper { get; set; } = "Assets/placeholderDefaultBackground.png";

    public Stretch DesktopWallpaperStretchMode { get; set; } = Stretch.UniformToFill;

    public Theme Theme { get; set; } = Theme.Dark;
}

public enum Theme // also edit SettingsViewModel
{
    Light,
    Dark
}
