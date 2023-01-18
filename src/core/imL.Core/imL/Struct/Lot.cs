namespace imL.Struct
{
    public struct Lot<G1, G2>
    {
        public G1 Lot1 { set; get; }
        public G2 Lot2 { set; get; }

        public Lot(G1 _lot1, G2 _lot2)
        {
            Lot1 = _lot1;
            Lot2 = _lot2;
        }
    }
}
