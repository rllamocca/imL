using imL.Contract.Hosting;
using imL.Rest.Frotcom;
using imL.Rest.Google;
using imL.Utility.Http;

namespace imL.Hosted.Frotcom.ToGPSChile
{
    public class Settings
    {
        public string GPSProvider { set; get; }
        public string GPSProviderRUT { set; get; }
        public string GPSProviderDV { set; get; }
        public string Supplier { set; get; }
        public string SupplierRUT { set; get; }
        public string SupplierDV { set; get; }

        public HostedSetting Hosted { set; get; }
        public FormatEndpoint Endpoint { set; get; }
        public FormatGoogle Google { set; get; }
        public FormatFrotcom Frotcom { set; get; }
    }

    public class HostedSetting : IPeriodSetting
    {
        public string[] Args { set; get; }
        public double Period { set; get; }
    }
}
