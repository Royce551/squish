using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Squish.Services;

public static class JsonService
{
    public static async Task<T> ReadAsync<T>(string path) where T : new()
    {
        if (!File.Exists(path))
        {
            await WriteAsync<T>(path);
        }

        using var jsonString = new FileStream(path, FileMode.OpenOrCreate);
        var thing = await JsonSerializer.DeserializeAsync<T>(jsonString);
        if (thing is not null) return thing;
        else throw new Exception(""); // TODO: exception message
    }

    public static async Task WriteAsync<T>(string path)
    {
        var jsonString = JsonSerializer.Serialize(typeof(T));
        await File.WriteAllTextAsync(path, jsonString);
    }
}