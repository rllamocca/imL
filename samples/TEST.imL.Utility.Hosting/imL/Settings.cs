using imL.Contract.Hosting;

namespace TEST.imL.Utility.Hosting
{
    public class Settings
    {
        public HostedSetting Hosted { set; get; }
    }

    public class HostedSetting : IPeriodSetting
    {
        public string[] Args { set; get; }
        public double Period { set; get; }
    }
}
