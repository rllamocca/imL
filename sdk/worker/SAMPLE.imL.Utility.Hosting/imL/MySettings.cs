using imL.Package.Hosting;

namespace SAMPLE.imL.Utility.Hosting
{
    internal class MySettings
    {
        public MyHostedSetting Hosted { set; get; }
    }

    internal class MyHostedSetting : IHostPeriodSetting
    {
        public double Period { set; get; }
        public double? Delay { set; get; }
    }
}
