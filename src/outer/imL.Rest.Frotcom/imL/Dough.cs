using imL.Rest.Frotcom.Schema;

namespace imL.Rest.Frotcom
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
