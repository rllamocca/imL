using imL.Rest.Frotcom.Schema;

namespace imL.Tool.Frotcom.ToGPSChile
{
    public class Dough
    {
        public Vehicle Vehicle { get; }
        public Location Location { get; }
        public Rest.Google.Schema.Maps.Result Result { get; }

        public Dough(
            Vehicle _vehicle,
            Location _location,
            Rest.Google.Schema.Maps.Result _result
            )
        {
            this.Vehicle = _vehicle;
            this.Location = _location;
            this.Result = _result;
        }
    }
}
