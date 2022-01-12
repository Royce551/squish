using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Squish.FreeDesktop;

public static class DesktopFileParser 
{

    /*
        Icon theme:

        usr/
            share/
                icons/
                    hicolor/
                        actions/
                            16/
                                action-new.png
                                document-save.png
                            32/
                                action-new.ong
                                document-save.png
                        categories/
                            16/
                                smartphone.png
                            32/
                                smartphone.png
                    contemporary/
                        actions/
                            16/
                                action-new.svg
                                document-save.svg
                            32/
                                document-save.svg
    */

    /*
        X11 _NET_WM_ICON
        
         h      l
        +--------+--------+--------+--------+            ------------------->
        | height |  width |aarrggbb|aarrggbb|            ------------------->
        +--------+--------+--------+--------+            ------------------->
        |aarrggbb|aarrggbb|aarrggbb|aarrggbb|            ------------------->
        +--------+--------+--------+--------+            ------------------->
        |aarrggbb|aarrggbb|aarrggbb|aarrggbb|            ------------------->
        +--------+--------+--------+--------+            ------------------->
        |aarrggbb|aarrggbb|aarrggbb|aarrggbb|            ------------------->
        +--------+--------+--------+--------+            ------------------->
        |      ...width x height times      |            ------------------->
        +--------+--------+--------+--------+            ------------------->
        |aarrggbb|aarrggbb|aarrggbb|aarrggbb|
        +--------+--------+--------+--------+
        | height |  width |aarrggbb|aarrggbb|
        +--------+--------+--------+--------+
        |aarrggbb|aarrggbb|aarrggbb|aarrggbb|
        +--------+--------+--------+--------+
        |aarrggbb|aarrggbb|aarrggbb|aarrggbb|
        +--------+--------+--------+--------+
        |aarrggbb|aarrggbb|aarrggbb|aarrggbb|
        +--------+--------+--------+--------+
        |      ...width x height times      |
        +--------+--------+--------+--------+
        |aarrggbb|aarrggbb|aarrggbb|aarrggbb|
        +--------+--------+--------+--------+
    */

    private static List<string> DefaultSearchPaths() 
    {
        var searchPaths = new List<string>
            {Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "/.local/share/applications")};

        searchPaths.AddRange(
            (Environment.GetEnvironmentVariable("XDG_DATA_DIRS")?.Split(":") ??
             new[] {"/usr/local/share", "/usr/share"}).Select(path => Path.Join(path, "applications")));
        return searchPaths;
    }

    public static async Task<DesktopFile[]> Applications(IEnumerable<string>? searchPaths = null) 
    {
        //TODO: filesystem listener for desktop files
        searchPaths ??= DefaultSearchPaths();

        var seenDesktopFiles = new HashSet<string>();
        return await Task.WhenAll(searchPaths
            .SelectMany(
                searchPath =>
                    Directory.EnumerateFiles(searchPath, "*", new EnumerationOptions() {RecurseSubdirectories = true}),
                        (searchPath, file) => (searchPath, file))
            .Select(spf => (spf, desktopEntry: Path.GetFileNameWithoutExtension(spf.file)))
            .Where(t => seenDesktopFiles.Add(t.desktopEntry))
            .Select(t => ParseFile(t.spf.file)));    
    }

    /// <summary>
    /// Reads and parses a Freedesktop .desktop file from the specified path.
    /// </summary>
    /// <param name="path"> Path of the .desktop file.</param>
    /// <returns>Parsed desktop file</returns>
    /// <exception cref="DesktopFileException">Thrown if file is invalid</exception>
    public static async Task<DesktopFile> ParseFile(string path) 
    {
        var lines = await File.ReadAllLinesAsync(path);
        var groups = new Dictionary<string, Dictionary<string, string>>();
        var readingGroup = "";
        Dictionary<string, string>? readingValues = null;
        for (var i = 0; i < lines.Length; i++)
        {
            var line = lines[i].Trim();
            if (line == "" || line.StartsWith('#'))
                continue;

            if (line.StartsWith('[') && line.EndsWith(']'))
            {
                var newGroup = line[1..^1].Trim();

                if (newGroup == readingGroup || groups.ContainsKey(newGroup))
                    throw new DesktopFileException($"Duplicate group [{newGroup}] in line {i}");

                if (readingValues is not null)
                    groups.Add(readingGroup, readingValues);
                

                readingGroup = newGroup;
                readingValues = new Dictionary<string, string>();
                continue;
            }

            if (readingValues is not null)
            {
                var ind = line.IndexOf('=');

                if (ind == -1)
                    throw new DesktopFileException($"Invalid entry in line {i}: entry does not contain equals sign");

                var (key, value) = (line[0..ind].Trim(), line[ind..].Trim());

                if (readingValues.ContainsKey(key))
                    throw new DesktopFileException($"Duplicate key {key} in group {readingGroup}, line {i}");

                readingValues[key] = value;
            }

            throw new DesktopFileException($"Invalid line {i}");
        }

        var desktopFile = new DesktopFile();

        if (groups.TryGetValue("Desktop Entry", out var values))
        {
            var nullCtx = new NullabilityInfoContext();

            foreach (var prop in typeof(DesktopFile).GetProperties())
            {
                var key = prop.Name;

                if (values.TryGetValue(prop.Name, out var value))
                {
                    static string EscapeDesktopString(string original) =>
                        original.Replace(@"\n", "\x0A").Replace(@"\s", " ").Replace(@"\r", "\x0D").Replace(@"\t", "\t").Replace(@"\\", @"\").Replace(@"\;", ";");

                    static CultureInfo ConvertToNetCulture(string original)
                    {
                        var onlyDashes = original.Replace('@', '-').Replace('_', '-');
                        var split = onlyDashes.Split(' ');
                        if (split.Length == 3)
                            return new CultureInfo($"{split[0]}-{split[2]}-{split[1]}");
                        else
                            return new CultureInfo(onlyDashes);
                    }

                    if (prop.PropertyType == typeof(string))
                    {
                        prop.SetValue(desktopFile, EscapeDesktopString(value));
                    }
                    else if (prop.PropertyType == typeof(string[]))
                    {
                        prop.SetValue(desktopFile, value.Split(';').Select(EscapeDesktopString).ToArray());
                    }
                    else if (prop.PropertyType == typeof(bool))
                    {
                        if (value is not "true" or "false")
                            throw new DesktopFileException($"Invalid value {value} for key of type boolean {key}");
                        prop.SetValue(desktopFile, value);
                    }
                    else if (prop.PropertyType == typeof(DesktopFileAction[]))
                    {
                        var actionStrings = value.Split(';').Select(EscapeDesktopString);
                        var actions = new List<DesktopFileAction>();
                        foreach (var actStr in actionStrings)
                        {
                            if (groups.TryGetValue($"Desktop Action {actStr}", out var actionDict))
                            {
                                actions.Add(new(actionDict.GetValueOrDefault("Name") ?? 
                                    throw new DesktopFileException($"Action {actStr} is missing required key Value"), 
                                    actionDict.GetValueOrDefault("Icon"), actionDict.GetValueOrDefault("Exec")));
                            }
                            else
                            {
                                throw new DesktopFileException($"Action {actStr} has no matching group");
                            }
                        }
                    }

                    //Locale strings
                    var regex = new Regex($@"{key}\[([A-z_@])+\]");
                    var defaultValue = value;
                    var otherLocales = values.Select(x => regex.Match(x.Key)).Where(x => x.Success);
                    var localesDict = otherLocales.Select(l => (ConvertToNetCulture(l.Groups[0].Value), values[l.Value]));
                    if (prop.PropertyType == typeof(LocaleStrings))
                    {
                        prop.SetValue(desktopFile, new LocaleStrings(defaultValue, localesDict.Select((kvp) => (kvp.Item1,
                            kvp.Item2.Split(';').Select(EscapeDesktopString).ToArray())).ToDictionary(x => x.Item1, x => x.Item2)));
                    }
                    else if (prop.PropertyType == typeof(LocaleString))
                    {
                        prop.SetValue(desktopFile, new LocaleString(defaultValue, localesDict.Select((kvp) => (kvp.Item1,
                            EscapeDesktopString(kvp.Item2))).ToDictionary(x => x.Item1, x => x.Item2)));
                    }
                }
                else
                {
                    var attr = Attribute.GetCustomAttribute(prop, typeof(DefaultValueIfNotPresentAttribute));
                    if (attr is DefaultValueIfNotPresentAttribute dva)
                    {
                        prop.SetValue(desktopFile, dva.DefaultValue);
                    }
                    else
                    {
                        var nullInfo = nullCtx.Create(prop);
                        if (nullInfo.ReadState == NullabilityState.Nullable)
                            prop.SetValue(desktopFile, null);
                        else
                            throw new DesktopFileException($"Required parameter {key} missing");
                    }
                }
            }

            return desktopFile;
        }
        throw new DesktopFileException("Invalid desktop file is missing Desktop Entry group");
    }


}