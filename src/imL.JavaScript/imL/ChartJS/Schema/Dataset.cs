namespace imL.JavaScript.ChartJS.Schema
{
    public class Dataset
    {
        public string label { get; set; }
        public int[] data { get; set; }
        public string[] backgroundColor { get; set; }
        public string[] borderColor { get; set; }
        public int? borderWidth { get; set; }

        public string stack { get; set; }

        public int? borderRadius { get; set; }
        public bool? borderSkipped { get; set; }

        public bool? fill { get; set; }
        public bool? stepped { get; set; }

        public decimal? tension { get; set; }

        public bool? hidden { get; set; }
}
}
