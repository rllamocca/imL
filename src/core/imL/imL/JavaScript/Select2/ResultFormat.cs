using imL.JavaScript.Select2.Schema;

namespace imL.JavaScript.Select2
{
#pragma warning disable IDE1006 // Estilos de nombres
    public record ResultFormat
    {
        public bool? more { set; get; }
        public Option[]? results { set; get; }
    }
#pragma warning restore IDE1006 // Estilos de nombres
}
