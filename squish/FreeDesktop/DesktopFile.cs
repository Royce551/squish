using System.Globalization;

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
    public string? Type { get; init; }
    public string? Version { get; init; }
    public LocaleString? Name { get; init; }
    public LocaleString? GenericName { get; init; }

    [DefaultValueIfNotPresent(false)]
    public bool NoDisplay { get; init; }

    public LocaleString? Comment { get; init; }
    public string? Icon { get; init; }

    [DefaultValueIfNotPresent(false)]
    public bool Hidden { get; init; }
    public string[]? OnlyShowIn { get; init; }
    public string[]? NotShowIn { get; init; }

    [DefaultValueIfNotPresent(false)]
    public bool DBusActivatable { get; init; }

    public string? TryExec { get; init; }
    public string? Exec { get; init; }
    public string? Path { get; init; }
    public bool? Terminal { get; init; }
    public DesktopFileAction[]? Actions { get; init; } //
    public string[]? MimeType { get; init; }
    public string[]? Categories { get; init; }
    public string[]? Implements { get; init; }
    public LocaleStrings? Keywords { get; init; }
    public Dictionary<string, string>? UnknownDesktopEntryKeys { get; init; }
    public Dictionary<string, string[]>? UnknownGroups { get; init; }
};

public record DesktopFileAction
(
    string Name,
    string? Icon,
    string? Exec
);

public class LocaleString : Dictionary<CultureInfo, string>
{
    public string Default { get; }
    public LocaleString(string @default, IDictionary<CultureInfo, string> dict) : base(dict)
    {
        Default = @default;
    }
}

public class LocaleStrings : Dictionary<CultureInfo, string[]>
{
    public string Default { get; }
    public LocaleStrings(string @default, IDictionary<CultureInfo, string[]> dict) : base(dict)
    {
        Default = @default;
    }
}

public class DesktopFileException : Exception
{
    public DesktopFileException(string message) : base(message) { }
}