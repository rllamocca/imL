using imL.Rest.Frotcom.Schema;

namespace imL.Rest.Frotcom
{
    public class Dough
    {
        public Vehicle200 Vehicle { get; }
        public Location200 Location { get; }

        public Dough(
            Vehicle200 _vehicle,
            Location200 _location
            )
        {
            Vehicle = _vehicle;
            Location = _location;
        }
    }
}
