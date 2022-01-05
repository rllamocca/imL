using imL.Contract;

namespace imL.Frotcom.Hosting.Core
{
    public class HostPeriodSetting : IHostPeriodSetting
    {
        public double Period { set; get; }
        public double Delay { set; get; }
    }
}
