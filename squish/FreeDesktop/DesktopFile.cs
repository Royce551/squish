using System;
using System.Collections.Generic;

namespace Squish.FreeDesktop;

[AttributeUsage(AttributeTargets.Parameter)]
internal class DefaultValueIfNotPresentAttribute<T> : Attribute
{
    public DefaultValueIfNotPresentAttribute(T defaultValue)
    {
        DefaultValue = defaultValue;
    }

    public T DefaultValue { get; } 
}

public record DesktopFile(
    string Type,
    string? Version,
    LocaleString Name,
    LocaleString? GenericName,
    [DefaultValueIfNotPresent<bool>(false)]
    bool NoDisplay,
    LocaleString? Comment,
    string? Icon,
    [DefaultValueIfNotPresent<bool>(false)]
    bool Hidden,
    string[]? OnlyShowIn,
    string[]? NotShowIn,
    [DefaultValueIfNotPresent<bool>(false)]
    bool DBusActivatable,
    string? TryExec,
    string? Exec,
    string? Path,
    bool? Terminal,
    DesktopFileAction[]? Actions, //
    string[]? MimeType,
    string[]? Categories,
    string[]? Implements,
    LocaleString[]? Keywords,
    Dictionary<string,string> UnknownDesktopEntryKeys,
    Dictionary<string, string[]> UnknownGroups
);

public record DesktopFileAction(
    string Name,
    string? Icon,
    string? Exec
)
