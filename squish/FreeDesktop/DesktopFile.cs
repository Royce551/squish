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
    string Type { get; set; }
    string? Version { get; set; }
    LocaleString Name { get; set; }
    LocaleString? GenericName { get; set; }
    [DefaultValueIfNotPresent(false)]
    bool NoDisplay { get; set; }
    LocaleString? Comment { get; set; }
    string? Icon { get; set; }
    [DefaultValueIfNotPresent(false)]
    bool Hidden { get; set; }
    string[]? OnlyShowIn { get; set; }
    string[]? NotShowIn { get; set; }
    [DefaultValueIfNotPresent(false)]
    bool DBusActivatable { get; set; }
    string? TryExec { get; set; }
    string? Exec { get; set; }
    string? Path { get; set; }
    bool? Terminal { get; set; }
    DesktopFileAction[]? Actions { get; set; } //
    string[]? MimeType { get; set; }
    string[]? Categories { get; set; }
    string[]? Implements { get; set; }
    LocaleString[]? Keywords { get; set; }
    Dictionary<string, string> UnknownDesktopEntryKeys { get; set; }
    Dictionary<string, string[]> UnknownGroups { get; set; }
};

public record DesktopFileAction(
    string Name,
    string? Icon,
    string? Exec
)
