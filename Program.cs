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
        //var result = ConfigurationHandler.CheckIfConfigFileExists();
        //var result = ConfigurationHandler.CreateNewDefaultConfigFile();
        //var result = ConfigurationHandler.GetAppConfiguration();
        //var result = ConfigurationHandler.GetConfigFilePath();
        //var result = ConfigurationHandler.LoadConfigFile();
        //var result = ConfigurationHandler.SaveConfigFile();

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

                if(loadedConfiguration.IsServerLocal && !File.Exists(loadedConfiguration.ServerFilePath))
                {
                    mainFilesErrors.Add($"SERVER: Local is flagged true but was not found in specified directory {loadedConfiguration.LauncherFilePath}.");
                }

                if (mainFilesErrors.Count == 0)
                {
                    List<App> apps = SetApps(loadedConfiguration);
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

        ////Run app
        //if (configErrors.Count == 0 && config != null)
        //{
        //    //Check if app location is in SPT directory
        //    if (File.Exists(Path.GetFileName(config.ServerFile)) && File.Exists(Path.GetFileName(config.LauncherFile)))
        //    {
        //        //Launch apps
        //        try
        //        {
        //            //Retrieve Server, Launcher and external apps
        //            List<App> apps = SetApps(config);
        //            //Tries to run each app subsequently, in order.
        //            foreach (App app in apps)
        //            {
        //                Console.WriteLine($"Launching {app.FilePath}...");
        //                if (Process.GetProcessesByName(Path.GetFileNameWithoutExtension(app.FilePath)).Length > 0)
        //                {
        //                    Console.WriteLine($"{app.FilePath} is already running, Skipping...");
        //                    app.Status = AppStatusEnum.AlreadyLaunched;
        //                }
        //                else
        //                {
        //                    if (!File.Exists(app.FilePath))
        //                    {
        //                        Console.WriteLine($"\"{app.FilePath}\" was not found. Skipping...");
        //                        app.Status = AppStatusEnum.NotFound;
        //                    }
        //                    else
        //                    {
        //                        //Launch apps minimized, or not.
        //                        string minSwitch = app.LaunchMinimized ? "/min" : string.Empty;
        //                        var filePath = @$"""{app.FilePath}""";
        //                        ProcessStartInfo startInfo = new()
        //                        {
        //                            FileName = app.FilePath,
        //                            Arguments = minSwitch,
        //                            UseShellExecute = true,
        //                            CreateNoWindow = false,
        //                            WindowStyle = app.LaunchMinimized ? ProcessWindowStyle.Minimized : ProcessWindowStyle.Normal
        //                        };
        //                        Process.Start(startInfo);

        //                        if (app.Type == AppTypeEnum.Server)
        //                        {
        //                            Console.ForegroundColor = ConsoleColor.DarkGray;
        //                            Console.WriteLine($"Waiting {config.ServerWaitTimeInSeconds} seconds for {app.FilePath} to finish initializing...");
        //                            Console.WriteLine($"(This value can be changed in the config file {config_file})");
        //                            Console.ResetColor();
        //                            Thread.Sleep(config.ServerWaitTimeInSeconds * 1000);
        //                        }
        //                        Console.WriteLine($"{app.FilePath} launched!");
        //                        app.Status = AppStatusEnum.Launched;
        //                    }
        //                }
        //            }

        //            //Assume success, otherwise errors would have been caught.
        //            SuccessEnd(apps);
        //        }
        //        catch (Exception ex)
        //        {
        //            WriteErrors(ex.ToString());
        //        }
        //    }
        //    else
        //    {
        //        WriteErrors($"{AppDomain.CurrentDomain.FriendlyName} must be in the same directory than {Path.GetFileName(config.ServerFile)} and {Path.GetFileName(config.LauncherFile)}");
        //    }
        //}
        //else
        //{
        //    WriteErrors(configErrors);
        //}
    }

    private static void SuccessEnd(List<App> apps)
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
            Console.WriteLine(string.Join(", ", appsLaunched.Select(x => x.FilePath)));
            Console.ForegroundColor = ConsoleColor.Green;
        }
        Console.WriteLine($"Apps Already Running (Skipped): {appsAlreadyLaunched.Count}");
        if (appsAlreadyLaunched.Count > 0)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(string.Join(", ", appsAlreadyLaunched.Select(x => x.FilePath)));
            Console.ForegroundColor = ConsoleColor.Green;
        }
        Console.WriteLine($"Apps Not Found (Skipped): {appsNotFound.Count}");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        if (appsNotFound.Count > 0)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(string.Join(", ", appsNotFound.Select(x => x.FilePath)));
            Console.ForegroundColor = ConsoleColor.Green;
        }
        Console.WriteLine(string.Empty);
        Console.ForegroundColor = ConsoleColor.Yellow;
        //TODO: config if app not found pause.
        Console.WriteLine($"{AppDomain.CurrentDomain.FriendlyName} will close itself in 5 seconds. GLHF!");
        Thread.Sleep(5000);
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

        if (!config.IsServerLocal) apps.Add(new App { FilePath = config.ServerFilePath, Type = AppTypeEnum.Server, LaunchMinimized = true });
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
        Version? version = assemblyName?.Version;

        if (version != null)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{AppDomain.CurrentDomain.FriendlyName} v{version}");
            Console.ResetColor();
        }
    }
}