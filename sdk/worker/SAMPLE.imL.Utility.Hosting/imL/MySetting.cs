using imL.Package.Hosting;

namespace SAMPLE.imL.Utility.Hosting
{
    internal class MySetting : IHostPeriodSetting
    {
        public double? Period { set; get; }
        public double? Delay { set; get; }

        public double? TimeOut { set; get; }
    }
}
