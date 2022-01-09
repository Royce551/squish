using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
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

    public static DesktopFile? ParseFile(string path) 
    {
        var x = "Hi";
        var lines = File.ReadAllLines(path);
        var groups = new Dictionary<string, Dictionary<string, string>>();
        var readingGroup = "";
        Dictionary<string, string>? readingValues = null;
        foreach (var l in lines)
        {
            var line = l.Trim();
            if (line == "" || line.StartsWith('#'))
                continue;

            if (line.StartsWith('[') && line.EndsWith(']'))
            {
                var newGroup = line[1..^1].Trim();

                if (newGroup == readingGroup || groups.ContainsKey(newGroup))
                    return null; 

                if (readingValues is not null)
                {
                    groups.Add(readingGroup, readingValues);
                }

                readingGroup = newGroup; // SCARUTCHO
                readingValues = new Dictionary<string, string>();
                continue;
            }

            if (readingValues is not null)
            {
                var ind = line.IndexOf('=');
                
                if (ind == -1)
                    return null;

                var (key, value) = (line[0..ind].Trim(), line[ind..].Trim());
                readingValues[key] = value;
            }

            return null; //file is invalid
        }

        var desktop = new DesktopFile();

        if (groups.TryGetValue("Desktop Entry", out var desktopEntryValues))
        {
            
            foreach (var prop in typeof(DesktopFile).GetProperties())
            {
                if (desktopEntryValues.ContainsKey(prop.Name))
                {
                    //TODO: parse
                }
                else
                {
                    var attr = Attribute.GetCustomAttribute(prop, typeof(DefaultValueIfNotPresentAttribute));
                    if (attr is DefaultValueIfNotPresentAttribute dva)
                        prop.SetValue(desktop, dva.DefaultValue);
                    else
                        prop.SetValue(desktop, null);
                }
            }
        }
    }
}