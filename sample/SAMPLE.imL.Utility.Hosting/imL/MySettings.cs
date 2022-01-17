using imL.Package.Hosting;

namespace SAMPLE.imL.Utility.Hosting
{
    internal class MySettings
    {
        public HostedSetting Hosted { set; get; }
    }

    internal class HostedSetting : IHostPeriodSetting
    {
        public double Period { set; get; }
        public double Delay { set; get; }
    }
}
