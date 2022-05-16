namespace imL.Rest.InsurDesign.Schema
{
#pragma warning disable IDE1006 // Estilos de nombres
    public class Login
    {
        public string expiry { get; set; }
        public string token { get; set; }
        public User user { get; set; }
    }
#pragma warning restore IDE1006 // Estilos de nombres
}
