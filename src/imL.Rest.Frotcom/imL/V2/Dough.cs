using imL.Rest.Frotcom.V2.Schema;

namespace imL.Rest.Frotcom.V2
{
    public class Dough
    {
        public Vehicle Vehicle { get; }
        public Location Location { get; }

        public Dough(
            Vehicle _vehicle,
            Location _location
            )
        {
            this.Vehicle = _vehicle;
            this.Location = _location;
        }
    }
}
