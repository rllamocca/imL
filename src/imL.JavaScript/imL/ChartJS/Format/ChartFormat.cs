namespace imL.JavaScript.ChartJS
{
    public class ChartFormat
    {
        public string Title { set; get; }
        public SerieFormat[] Series { set; get; }
        public AxisFormat XAxis { set; get; }
        public AxisFormat YAxis { set; get; }
        public AxisFormat ZAxis { set; get; }
        public string[] BackgroundColor { set; get; }
        public string[] BorderColor { set; get; }
    }
}