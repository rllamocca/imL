namespace imL.Rest.Google.Schema.Maps
{
#pragma warning disable IDE1006 // Estilos de nombres
    public class Result
    {
        public Address_Components[] address_components { set; get; }
        public string formatted_address { set; get; }
        public Geometry geometry { set; get; }
        public string place_id { set; get; }
        public Plus_Code plus_code { set; get; }
        public string[] types { set; get; }
    }
#pragma warning restore IDE1006 // Estilos de nombres
}