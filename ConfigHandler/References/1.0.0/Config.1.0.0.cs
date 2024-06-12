namespace NotIncluded_100
{
    public class Config
    {
        public int ServerWaitTimeInSeconds { get; set; }
        public string ServerFile { get; set; }
        public string LauncherFile { get; set; }
        public List<ExternalApp> ExternalApps { get; set; }
    }

    public class ExternalApp
    {
        public string File { get; set; }
        public bool LaunchMinimized { get; set; }
    }
}