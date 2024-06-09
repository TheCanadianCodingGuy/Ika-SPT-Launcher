using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

class Program
{
    static void Main()
    {
        int timeoutSeconds = 15;
        string[] apps = { "Aki.Server.exe", "Aki.Launcher.exe" };

        foreach (string app in apps)
        {
            Console.WriteLine($"Starting {app}...");

            if (Process.GetProcessesByName(Path.GetFileNameWithoutExtension(app)).Length > 0)
            {
                Console.WriteLine($"{app} is already running, Skipping...");
            }
            else
            {
                bool run = true;

                if (!File.Exists(app))
                {
                    Console.WriteLine($"\"{app}\" does not exist. Skipping...");
                    run = false;
                }

                if (run)
                {
                    if (app == "Aki.Launcher.exe")
                    {
                        Process.Start(app);
                    }
                    else
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo
                        {
                            FileName = "cmd.exe",
                            Arguments = $"/c start /min {app}",
                            WindowStyle = ProcessWindowStyle.Hidden
                        };
                        Process.Start(startInfo);
                    }

                    if (app == "Aki.Server.exe")
                    {
                        Console.WriteLine($"Waiting {timeoutSeconds} seconds for server to finish initializing...");
                        Thread.Sleep(timeoutSeconds * 1000);
                    }
                }
            }
        }
    }
}
