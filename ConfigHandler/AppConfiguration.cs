namespace ConfigHandler
{
    public class AppConfiguration
    {
        public double ConfigurationRevision { get; set; } = 2406.01;
        public int ServerWaitTimeInSeconds { get; set; } = 10;
        public bool IsServerLocal { get; set; } = true;
        public bool PauseIfAppsNotFound { get; set; } = false;
        public string ServerFilePath { get; set; } = @"C:\SPT\Aki.Server.exe";
        public string LauncherFilePath { get; set; } = @"C:\SPT\Aki.Launcher.exe";
        public List<ExternalApp> ExternalApps { get; set; } = [];
    }

    public class ExternalApp
    {
        public string FilePath { get; set; }
        public bool LaunchMinimized { get; set; }
    }
}