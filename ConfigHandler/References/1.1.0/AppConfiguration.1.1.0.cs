namespace NotIncluded_110
{
    public class AppConfiguration
    {
        public int ServerWaitTimeInSeconds { get; set; } = 10;
        public bool IsServerLocal { get; set; } = true;
        public string ServerFilePath { get; set; } = "Aki.Server.exe";
        public string LauncherFilePath { get; set; } = "Aki.Launcher.exe";
        public List<ExternalApp> ExternalApps { get; set; } = [];
    }

    public class ExternalApp
    {
        public string FilePath { get; set; }
        public bool LaunchMinimized { get; set; }
    }
}