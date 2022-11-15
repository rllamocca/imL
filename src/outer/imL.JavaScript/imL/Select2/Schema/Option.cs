namespace imL.JavaScript.Select2.Schema
{
#pragma warning disable IDE1006 // Estilos de nombres
    public class Option
    {
        public string id { set; get; }
        public string text { set; get; }
        public bool? selected { set; get; }
        public bool? disabled { set; get; }

        public Option[] children { set; get; }

        public string code { set; get; }
    }
#pragma warning restore IDE1006 // Estilos de nombres
}
