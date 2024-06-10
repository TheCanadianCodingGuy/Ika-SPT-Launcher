using System.Diagnostics;
using System.Reflection;
using Ika_SPT_Launcher;
using Newtonsoft.Json;

class Program
{
    static void Main()
    {
        List<string> configErrors = [];
        Config? config = new();

        //Config Info
        string config_file = $"{AppDomain.CurrentDomain.FriendlyName}.config.json";
        int DEAULT_SERVER_WAIT_TIME = 10;
        string DEFAULT_SERVER_FILE = "Aki.Server.exe";
        string DEFAULT_LAUNCHER_FILE = "Aki.Launcher.exe";

        DisplayAppVersion();

        try
        {
            //Check if app location is in SPT directory
            if (File.Exists(DEFAULT_SERVER_FILE) && File.Exists(DEFAULT_LAUNCHER_FILE))
            {
                //Check if config exists, if not, create a default config
                if (!File.Exists(config_file))
                {
                    config = new Config()
                    {
                        ServerWaitTimeInSeconds = DEAULT_SERVER_WAIT_TIME,
                        ServerFile = DEFAULT_SERVER_FILE,
                        LauncherFile = DEFAULT_LAUNCHER_FILE,
                        ExternalApps = [new ExternalApp { File = string.Empty, LaunchMinimized = true }]
                    };

                    string newConfigJson = JsonConvert.SerializeObject(config, Formatting.Indented);
                    string appPath = AppDomain.CurrentDomain.BaseDirectory;
                    string newSavefilePath = Path.Combine(appPath, config_file);
                    File.WriteAllText(newSavefilePath, newConfigJson);
                    Console.WriteLine($"No config file found, {newSavefilePath} created with default values. You can edit the values and add external apps to load in this file.");
                }

                string? jsonContent = File.ReadAllText(config_file);
                if (jsonContent != null)
                {
                    config = JsonConvert.DeserializeObject<Config>(jsonContent);

                    if (config != null)
                    {
                        if (config.ServerWaitTimeInSeconds < 0 || config.ServerWaitTimeInSeconds > 600) configErrors.Add("config.ServerWaitTimeInSeconds: Invalid Value, Must be between 0 and 600.");
                        if (string.IsNullOrWhiteSpace(config.ServerFile) || !config.ServerFile.EndsWith(".exe")) configErrors.Add("config.ServerFile: Invalid file path and/or name.");
                        if (string.IsNullOrWhiteSpace(config.LauncherFile) || !config.LauncherFile.EndsWith(".exe")) configErrors.Add("config.LauncherFile: Invalid file path and/or name.");
                        config.ExternalApps ??= [];
                    }
                    else
                    {
                        configErrors.Add($"{config_file} was impropriately deserialized and returned null.");
                    }
                }
                else
                {
                    configErrors.Add($"{config_file} could not be deserialized. It is either corrupted or missing.");
                }
            }
            else
            {
                WriteErrors($"{AppDomain.CurrentDomain.FriendlyName} must be in the same directory than {DEFAULT_SERVER_FILE} and {DEFAULT_LAUNCHER_FILE}");
            }
        }
        catch (Exception ex)
        {
            configErrors.Add(ex.ToString());
        }

        //Run app
        if (configErrors.Count == 0 && config != null)
        {
            //Check if app location is in SPT directory
            if (File.Exists(Path.GetFileName(config.ServerFile)) && File.Exists(Path.GetFileName(config.LauncherFile)))
            {
                //Launch apps
                try
                {
                    //Retrieve Server, Launcher and external apps
                    List<App> apps = SetApps(config);
                    //Tries to run each app subsequently, in order.
                    foreach (App app in apps)
                    {
                        Console.WriteLine($"Launching {app.FilePath}...");
                        if (Process.GetProcessesByName(Path.GetFileNameWithoutExtension(app.FilePath)).Length > 0)
                        {
                            Console.WriteLine($"{app.FilePath} is already running, Skipping...");
                            app.Status = AppStatus.AlreadyLaunched;
                        }
                        else
                        {
                            if (!File.Exists(app.FilePath))
                            {
                                Console.WriteLine($"\"{app.FilePath}\" was not found. Skipping...");
                                app.Status = AppStatus.NotFound;
                            }
                            else
                            {
                                //Launch apps minimized, or not.
                                string minSwitch = app.LaunchMinimized ? "/min" : string.Empty;
                                var filePath = @$"""{app.FilePath}""";
                                ProcessStartInfo startInfo = new()
                                {
                                    FileName = app.FilePath,
                                    Arguments = minSwitch,
                                    UseShellExecute = true,
                                    CreateNoWindow = false,
                                    WindowStyle = app.LaunchMinimized ? ProcessWindowStyle.Minimized : ProcessWindowStyle.Normal
                                };
                                Process.Start(startInfo);

                                if (app.Type == AppType.Server)
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                    Console.WriteLine($"Waiting {config.ServerWaitTimeInSeconds} seconds for {app.FilePath} to finish initializing...");
                                    Console.WriteLine($"(This value can be changed in the config file {config_file})");
                                    Console.ResetColor();
                                    Thread.Sleep(config.ServerWaitTimeInSeconds * 1000);
                                }
                                Console.WriteLine($"{app.FilePath} launched!");
                                app.Status = AppStatus.Launched;
                            }
                        }
                    }

                    //Assume success, otherwise errors would have been caught.
                    SuccessEnd(apps);
                }
                catch (Exception ex)
                {
                    WriteErrors(ex.ToString());
                }
            }
            else
            {
                WriteErrors($"{AppDomain.CurrentDomain.FriendlyName} must be in the same directory than {Path.GetFileName(config.ServerFile)} and {Path.GetFileName(config.LauncherFile)}");
            }
        }
        else
        {
            WriteErrors(configErrors);
        }
    }

    private static void SuccessEnd(List<App> apps)
    {
        List<App> appsLaunched = apps.Where(x => x.Status == AppStatus.Launched).ToList();
        List<App> appsAlreadyLaunched = apps.Where(x => x.Status == AppStatus.AlreadyLaunched).ToList();
        List<App> appsNotFound = apps.Where(x => x.Status == AppStatus.NotFound).ToList();

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
        Console.WriteLine($"{AppDomain.CurrentDomain.FriendlyName} will close itself in 5 seconds. GLHF!");
        Console.ResetColor();
        Thread.Sleep(5000);
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

    private static List<App> SetApps(Config config)
    {
        List<App> apps =
        [
            new App{ FilePath = config.ServerFile, Type = AppType.Server, LaunchMinimized = true },
            new App{ FilePath = config.LauncherFile, Type = AppType.Launcher, LaunchMinimized = false }
        ];

        foreach (ExternalApp app in config.ExternalApps.Where(x => !string.IsNullOrWhiteSpace(x.File)))
        {
            apps.Add(new App
            {
                FilePath = app.File,
                Type = AppType.External,
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
        if (lastPeriodIndex != -1)
        {
            Console.WriteLine($"{AppDomain.CurrentDomain.FriendlyName} v{version[..lastPeriodIndex]}");
            Console.ResetColor();
        }
    }
}