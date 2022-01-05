namespace imL.JavaScript.ChartJS.Schema
{
#pragma warning disable IDE1006 // Estilos de nombres
    public class Y
    {
        public Title title { set; get; }
        public Grid grid { set; get; }

        public bool stacked { set; get; }
        public bool beginAtZero { set; get; }

        public string type { set; get; }
        public bool? display { set; get; }
        public string position { set; get; }

        public int? suggestedMin { set; get; }
        public int? suggestedMax { set; get; }
    }
#pragma warning restore IDE1006 // Estilos de nombres
}