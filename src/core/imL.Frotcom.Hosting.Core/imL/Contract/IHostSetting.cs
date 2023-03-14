using imL.Package.Hosting;
using imL.Rest.Frotcom;

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
