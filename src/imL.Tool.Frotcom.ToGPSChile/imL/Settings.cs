using imL.Rest.Frotcom;
using imL.Rest.Google;
using imL.Utility.Http;

namespace imL.Tool.Frotcom.ToGPSChile
{
    public class Settings
    {
        public string GPSProvider { set; get; }
        public string GPSProviderRUT { set; get; }
        public string GPSProviderDV { set; get; }
        public string Supplier { set; get; }
        public string SupplierRUT { set; get; }
        public string SupplierDV { set; get; }

        public FormatEndpoint Endpoint { set; get; }
        public FormatGoogle Google { set; get; }
        public FormatFrotcom Frotcom { set; get; }
    }
}
