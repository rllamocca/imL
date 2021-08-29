namespace imL.JavaScript.ChartJS.Schema
{
    public class Options
    {
        public Scales scales { get; set; }

        public bool? responsive { get; set; }
        public Plugins plugins { get; set; }

        public string indexAxis { get; set; }
        public Elements elements { get; set; }

        public Interaction interaction { get; set; }
    }
}