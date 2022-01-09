using System;
using System.Collections.Generic;

namespace Squish.FreeDesktop;

[AttributeUsage(AttributeTargets.Property)]
internal class DefaultValueIfNotPresentAttribute : Attribute
{
    public DefaultValueIfNotPresentAttribute(object defaultValue)
    {
        DefaultValue = defaultValue;
    }

    public object DefaultValue { get; } 
}

public class DesktopFile
{
    public string? Type { get; set; }
    public string? Version { get; set; }
    public LocaleString? Name { get; set; }
    public LocaleString? GenericName { get; set; }

    [DefaultValueIfNotPresent(false)]
    public bool NoDisplay { get; set; }

    public LocaleString? Comment { get; set; }
    public string? Icon { get; set; }

    [DefaultValueIfNotPresent(false)]
    public bool Hidden { get; set; }
    public string[]? OnlyShowIn { get; set; }
    public string[]? NotShowIn { get; set; }

    [DefaultValueIfNotPresent(false)]
    public bool DBusActivatable { get; set; }

    public string? TryExec { get; set; }
    public string? Exec { get; set; }
    public string? Path { get; set; }
    public bool? Terminal { get; set; }
    public DesktopFileAction[]? Actions { get; set; } //
    public string[]? MimeType { get; set; }
    public string[]? Categories { get; set; }
    public string[]? Implements { get; set; }
    public LocaleString[]? Keywords { get; set; }
    public Dictionary<string, string>? UnknownDesktopEntryKeys { get; set; }
    public Dictionary<string, string[]>? UnknownGroups { get; set; }
};

public record DesktopFileAction
(
    string Name,
    string? Icon,
    string? Exec
);
