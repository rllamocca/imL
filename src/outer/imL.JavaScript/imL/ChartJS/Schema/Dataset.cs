namespace imL.JavaScript.ChartJS.Schema
{
#pragma warning disable IDE1006 // Estilos de nombres
    public class Dataset
    {
        public string label { set; get; }
        public decimal?[] data { set; get; }
        public string[] backgroundColor { set; get; }
        public string[] borderColor { set; get; }
        public int? borderWidth { set; get; }

        public string stack { set; get; }

        public int? borderRadius { set; get; }
        public bool? borderSkipped { set; get; }

        public bool? fill { set; get; }
        public bool? stepped { set; get; }

        public decimal? tension { set; get; }

        public bool? hidden { set; get; }

        public string yAxisID { set; get; }

        public string cubicInterpolationMode { set; get; }

        public int?[] borderDash { set; get; }

        public string type { set; get; }

        public int? order { set; get; }
    }
#pragma warning restore IDE1006 // Estilos de nombres
}
