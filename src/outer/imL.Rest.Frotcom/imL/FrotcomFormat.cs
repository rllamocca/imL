using System.Collections.Generic;

namespace imL.Rest.Frotcom
{
    public class FrotcomFormat
    {
        public string URI { set; get; }
        public AuthorizeFormat Authorize { set; get; }

        public IEnumerable<string> LicensePlates { set; get; }
    }
}
