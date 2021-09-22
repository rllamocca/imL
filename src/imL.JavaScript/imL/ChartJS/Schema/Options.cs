namespace imL.JavaScript.ChartJS.Schema
{
    public class Options
    {
        public Scales scales { set; get; }

        public bool? responsive { set; get; }
        public Plugins plugins { set; get; }

        public string indexAxis { set; get; }
        public Elements elements { set; get; }

        public Interaction interaction { set; get; }
    }
}