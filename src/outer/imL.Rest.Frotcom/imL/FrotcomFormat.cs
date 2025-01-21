using System.Collections.Generic;

using imL.Rest.Frotcom.Schema;

namespace imL.Rest.Frotcom
{
    public class FrotcomFormat
    {
        public string URI { set; get; }
        public double? LifeTime { set; get; }
        public Authorize User { set; get; }

        public IEnumerable<string> LicensePlates { set; get; }
    }
}
