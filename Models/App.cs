namespace Ika_SPT_Launcher.Models
{
    internal class App
    {
        public string FilePath { get; set; }
        public AppTypeEnum Type { get; set; }
        public AppStatusEnum Status { get; set; }
        public bool LaunchMinimized { get; set; }
    }
}
