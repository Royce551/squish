namespace Squish.Models;

public class TaskbarWindow
{
    public string Id { get; init; } = "lol";

    public string Title { get; init; } = "Window";

    public byte[]? Icon { get; init; }

    public bool IsActiveWindow { get; set; } = false;
}
