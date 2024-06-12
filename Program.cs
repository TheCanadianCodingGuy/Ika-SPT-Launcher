using System.Diagnostics;
using System.Reflection;
using Ika_SPT_Launcher.Models;
using ConfigHandler;

class Program
{
    static void Main()
    {
        //TODO: DO BACKWARD COMPATIBILITY FOR OLD CONFIGS
        DisplayAppVersion();
        ConfigurationHandler.CheckIfConfigurationIsLatestRevision();
        //TODO: Method to go through revisions.
        try
        {
            var resultLoadConfigFile = ConfigurationHandler.LoadConfigFile();
            if (resultLoadConfigFile.Item1.IsSuccess)
            {
                AppConfiguration loadedConfiguration = resultLoadConfigFile.Item2;
                List<string> mainFilesErrors = [];

                if (!File.Exists(loadedConfiguration.LauncherFilePath))
                {
                    mainFilesErrors.Add($"LAUNCHER: {loadedConfiguration.LauncherFilePath} was not found.");
                }

                if (loadedConfiguration.IsServerLocal && !File.Exists(loadedConfiguration.ServerFilePath))
                {
                    mainFilesErrors.Add($"SERVER: Local = Yes");
                    mainFilesErrors.Add($"SERVER: {loadedConfiguration.LauncherFilePath} was not found.");
                }

                if (mainFilesErrors.Count == 0)
                {
                    List<App> apps = SetApps(loadedConfiguration);
                    //Tries to run each app subsequently, in order.

                    Console.WriteLine($"SERVER: Local = {(loadedConfiguration.IsServerLocal ? "Yes" : "No, Skipping...")}");

                    foreach (App app in apps)
                    {
                        Console.WriteLine($"Launching {app.FilePath}...");
                        if (Process.GetProcessesByName(Path.GetFileNameWithoutExtension(app.FilePath)).Length > 0)
                        {
                            Console.WriteLine(@$"""{app.FilePath}"" is already running, Skipping...");
                            app.Status = AppStatusEnum.AlreadyLaunched;
                        }
                        else
                        {
                            if (!File.Exists(app.FilePath))
                            {
                                Console.WriteLine(@$"""{app.FilePath}"" was not found. Skipping...");
                                app.Status = AppStatusEnum.NotFound;
                            }
                            else
                            {
                                //Launch apps minimized, or not.
                                string minSwitch = app.LaunchMinimized ? "/min" : string.Empty;
                                var filePath = @$"{app.FilePath}";

                                ProcessStartInfo startInfo = new()
                                {
                                    FileName = app.FilePath,
                                    Arguments = minSwitch,
                                    UseShellExecute = true,
                                    CreateNoWindow = false,
                                    WorkingDirectory = Path.GetDirectoryName(app.FilePath),
                                    WindowStyle = app.LaunchMinimized ? ProcessWindowStyle.Minimized : ProcessWindowStyle.Normal
                                };
                                Process.Start(startInfo);

                                if (app.Type == AppTypeEnum.Server)
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.WriteLine(@$"Waiting {loadedConfiguration.ServerWaitTimeInSeconds} seconds for ""{app.FilePath}"" to finish initializing...");
                                    Console.WriteLine($"(This value can be changed in the config file.)");
                                    Console.ResetColor();
                                    Thread.Sleep(loadedConfiguration.ServerWaitTimeInSeconds * 1000);
                                }

                                Console.WriteLine($"{app.FilePath} launched!");
                                app.Status = AppStatusEnum.Launched;
                            }
                        }
                    }

                    //Assume success, otherwise errors would have been caught.
                    SuccessEnd(apps, loadedConfiguration.PauseIfAppsNotFound);
                }
                else
                {
                    WriteErrors(mainFilesErrors);
                }
            }
            else WriteErrors(resultLoadConfigFile.Item1.Message);

        }
        catch (Exception ex)
        {
            WriteErrors(ex.ToString());
        }
    }

    private static void SuccessEnd(List<App> apps, bool pauseIfAppsNotFound)
    {
        List<App> appsLaunched = apps.Where(x => x.Status == AppStatusEnum.Launched).ToList();
        List<App> appsAlreadyLaunched = apps.Where(x => x.Status == AppStatusEnum.AlreadyLaunched).ToList();
        List<App> appsNotFound = apps.Where(x => x.Status == AppStatusEnum.NotFound).ToList();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(string.Empty);
        Console.WriteLine("SUCCESS!");
        Console.WriteLine($"Apps Launched: {appsLaunched.Count}");
        if (appsLaunched.Count > 0)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            foreach (var app in appsLaunched) Console.WriteLine(app.FilePath);
            Console.ForegroundColor = ConsoleColor.Green;
        }
        Console.WriteLine($"Apps Already Running (Skipped): {appsAlreadyLaunched.Count}");
        if (appsAlreadyLaunched.Count > 0)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            foreach (var app in appsAlreadyLaunched) Console.WriteLine(app.FilePath);
            Console.ForegroundColor = ConsoleColor.Green;
        }
        Console.WriteLine($"Apps Not Found (Skipped): {appsNotFound.Count}");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        if (appsNotFound.Count > 0)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            foreach (var app in appsNotFound) Console.WriteLine(app.FilePath);
            Console.ForegroundColor = ConsoleColor.Green;
        }
        Console.WriteLine(string.Empty);
        Console.ForegroundColor = ConsoleColor.Yellow;
        if (appsNotFound.Count > 0 && pauseIfAppsNotFound)
        {
            Console.WriteLine($"Pause if apps were not found is enabled in the configuration file. When you are ready, press enter to exit.");
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine($"{AppDomain.CurrentDomain.FriendlyName} will close itself in 5 seconds. GLHF!");
            Thread.Sleep(5000);
        }
        Console.ResetColor();
    }

    private static void WriteErrors(List<string> errors)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        foreach (string error in errors)
        {
            Console.Error.WriteLine(error);
        }
        Console.ResetColor();
        Console.WriteLine(string.Empty);
        Console.WriteLine("Press Enter to exit...");
        Console.ReadLine();
    }

    private static void WriteErrors(string errors)
    {
        WriteErrors([errors]);
    }

    private static List<App> SetApps(AppConfiguration config)
    {
        List<App> apps = [];

        if (config.IsServerLocal) apps.Add(new App { FilePath = config.ServerFilePath, Type = AppTypeEnum.Server, LaunchMinimized = true });
        apps.Add(new App { FilePath = config.LauncherFilePath, Type = AppTypeEnum.Launcher, LaunchMinimized = false });

        foreach (ExternalApp app in config.ExternalApps.Where(x => !string.IsNullOrWhiteSpace(x.FilePath)))
        {
            apps.Add(new App
            {
                FilePath = app.FilePath,
                Type = AppTypeEnum.External,
                LaunchMinimized = app.LaunchMinimized
            });
        }
        return apps;
    }

    private static void DisplayAppVersion()
    {
        Assembly? assembly = Assembly.GetEntryAssembly();
        AssemblyName? assemblyName = assembly?.GetName();
        string? version = assemblyName?.Version?.ToString();
        version ??= "0.0.0.0";
        int lastPeriodIndex = version.LastIndexOf('.');
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"{AppDomain.CurrentDomain.FriendlyName} v{version[..lastPeriodIndex]}");
        Console.ResetColor();
    }
}