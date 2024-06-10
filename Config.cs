namespace Ika_SPT_Launcher
{
    internal class Config
    {
        public int ServerWaitTimeInSeconds { get; set; }
        public string ServerFile { get; set; }
        public string LauncherFile { get; set; }
        public List<ExternalApp> ExternalApps { get; set; }
    }

    internal class ExternalApp
    {
        public string File { get; set; }
        public bool LaunchMinimized { get; set; }
    }
}