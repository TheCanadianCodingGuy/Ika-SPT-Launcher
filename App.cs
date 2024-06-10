namespace Ika_SPT_Launcher
{
    internal class App
    {
        public string FilePath { get; set; }
        public AppType Type { get; set; }
        public AppStatus Status { get; set; }
        public bool LaunchMinimized { get; set; }
    }

    internal enum AppStatus
    {
        Launched,
        AlreadyLaunched,
        NotFound
    }

    internal enum AppType
    {
        Server,
        Launcher,
        External
    }
}
