namespace imL.Rest.Google.Schema.Maps
{
#pragma warning disable IDE1006 // Estilos de nombres
    public class Geometry
    {
        public Location location { set; get; }
        public string location_type { set; get; }
        public Viewport viewport { set; get; }
        public Bounds bounds { set; get; }
    }
#pragma warning restore IDE1006 // Estilos de nombres
}