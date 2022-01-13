using imL.Format;
using imL.Rest.Frotcom;
using imL.Utility.Hosting;

namespace imL.Frotcom.Hosting.Core
{
    public interface IHostSetting : IHostPeriodSetting
    {
        EndpointFormat Endpoint { set; get; }
        FrotcomFormat Frotcom { set; get; }

        //double Period { set; get; }
        //double Delay { set; get; }
    }
}
