using imL;
using imL.Frotcom.Hosting.Core;
using imL.Package.Hosting;
using imL.Rest.Frotcom;

namespace SAMPLE.imL.Frotcom.Hosting.Core
{
    internal class MySetting : IHostSetting, IHostPeriodSetting
    {
        public EndpointFormat Endpoint { set; get; }
        public FrotcomFormat Frotcom { set; get; }

        public double? Period { set; get; }
        public double? TimeOut { set; get; }
    }
}
