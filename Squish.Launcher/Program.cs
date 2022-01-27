// This app manages Squish in Linux environments to prevent unexpected logouts if Squish crashes

using System.Diagnostics;

while (true)
{
    var squish = new Process
    {
        StartInfo = new()
        {
            FileName = "Squish", // for now we're assuming we're next to squish, maybe add cli arg later?
            Arguments = string.Join(" ", args)
        }
    };

    squish.Start();
    await squish.WaitForExitAsync();

    if (squish.ExitCode != 0)
    {
        Console.WriteLine("looks like squish crashed :(");
        Process.Start("Squish", "--error-recovery");
    }
    else return;
}


