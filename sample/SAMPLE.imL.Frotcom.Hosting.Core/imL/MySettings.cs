using imL.Utility.Hosting;

namespace SAMPLE.imL.Frotcom.Hosting.Core
{
    internal class MySettings
    {
        public HostedSetting? Hosted { set; get; }
    }

    internal class HostedSetting : IHostPeriodSetting
    {
        public double Period { set; get; }
        public double Delay { set; get; }
    }
}
