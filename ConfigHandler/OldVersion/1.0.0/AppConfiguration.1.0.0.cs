namespace NotIncluded_100
{
    public class AppConfiguration100
    {
        public int ServerWaitTimeInSeconds { get; set; }
        public string ServerFile { get; set; }
        public string LauncherFile { get; set; }
        public List<ExternalApp100> ExternalApps { get; set; }
    }

    public class ExternalApp100
    {
        public string File { get; set; }
        public bool LaunchMinimized { get; set; }
    }
}
