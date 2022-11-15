namespace imL.JavaScript.Select2.Schema
{
#pragma warning disable IDE1006 // Estilos de nombres
    public class Query
    {
        public int? page { set; get; }

#if NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER
        public string? term { set; get; }
        public string? type { set; get; }
#else
        public string term { set; get; }
        public string type { set; get; }
#endif

    }
#pragma warning restore IDE1006 // Estilos de nombres
}
