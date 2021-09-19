namespace imL.JavaScript.ChartJS.Schema
{
    public class Dataset
    {
        public string label { set; get; }
        public int[] data { set; get; }
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
}
}
