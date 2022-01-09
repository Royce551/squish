using System.Reflection;

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

    public static List<string> DefaultSearchPaths() 
    {
        var searchPaths = new List<string>();
        searchPaths.Add(Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "/.local/share/applications"));

        foreach (string path in Environment.GetEnvironmentVariable("XDG_DATA_DIRS")?.Split(":") ?? new[] { "/usr/local/share", "/usr/share" }) 
        {
            searchPaths.Add(Path.Join(path, "applications"));
        }
        return searchPaths;
    }

    public static List<DesktopFile> Applications(List<string> searchPaths = null) 
    {
        //TODO: filesyste, listener for desktop files
        if (searchPaths == null) 
        {
            searchPaths = DefaultSearchPaths();
        }

        var desktopFiles = new List<DesktopFile>();
        var seenDesktopFiles = new HashSet<string>();
        foreach (string searchPath in searchPaths)
        {
            foreach (var file in Directory.EnumerateFiles(searchPath, "*", new EnumerationOptions() { RecurseSubdirectories = true })) 
            {
                var desktopEntry = Path.GetFileNameWithoutExtension(file);
                if (!seenDesktopFiles.Add(desktopEntry))
                { 
                    continue;
                }

                var desktopFile = ParseFile(file);
                if (desktopFile == null) continue;
                desktopFiles.Add(desktopFile);
            }
        }
        return desktopFiles;
    }


    /// <summary>
    /// Reads and parses a Freedesktop .desktop file from the specified path.
    /// </summary>
    /// <param name="path"> Path of the .desktop file.</param>
    /// <returns>Parsed desktop file</returns>
    /// <exception cref="DesktopFileException">Thrown if file is invalid</exception>
    public static DesktopFile ParseFile(string path) 
    {
        var x = "Hi";
        var lines = File.ReadAllLines(path);
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
                

                readingGroup = newGroup; // SCARUTCHO
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
                
                if (key.EndsWith(']')) 
                    continue;

                if (values.TryGetValue(prop.Name, out var value))
                {
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
                    //implement locale strings
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
        }
    }

    static string EscapeDesktopString(string original) =>
        original.Replace(@"\n", "\x0A").Replace(@"\s", " ").Replace(@"\r", "\x0D").Replace(@"\t", "\t").Replace(@"\\", @"\").Replace(@"\;", ";");
}