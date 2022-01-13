using imL.Format;
using imL.Rest.Frotcom;
using imL.Utility.Hosting;

namespace imL.Frotcom.Hosting.Core
{
    public class HostSettingDefault : IHostSetting, IHostPeriodSetting
    {
        public EndpointFormat Endpoint { set; get; }
        public FrotcomFormat Frotcom { set; get; }

        public double Period { set; get; }
        public double Delay { set; get; }
    }
}
