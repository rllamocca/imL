using System.Collections.Generic;

namespace imL.Struct
{
    public struct OneToMany<G1, G2>
    {
        public G1 One { set; get; }
        public IEnumerable<G2> Many { set; get; }

        public OneToMany(G1 _one, IEnumerable<G2> _many)
        {
            this.One = _one;
            this.Many = _many;
        }
    }
}
