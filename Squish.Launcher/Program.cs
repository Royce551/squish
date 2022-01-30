// This app manages Squish in Linux environments to prevent unexpected logouts if Squish crashes

using System.Diagnostics;

var allArgs = new List<string>(args);

int errorCount = 0;

while (true)
{
    var squish = new Process
    {
        StartInfo = new()
        {
            FileName = "Squish", // maybe add cli arg for starting squish in an specified place later?
            Arguments = string.Join(" ", allArgs)
        }
    };

    squish.Start();
    await squish.WaitForExitAsync();

    if (squish.ExitCode != 0)
    {
        Console.WriteLine("squish crashed :(");
        if (errorCount < 1) allArgs.Add("--error-occurred");
        else allArgs.Add("--error-recovery");
        errorCount++;
    }
    else return;
}


