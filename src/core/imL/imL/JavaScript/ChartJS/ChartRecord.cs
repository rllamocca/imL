namespace imL.JavaScript.ChartJS
{
    public record ChartRecord
    {
        public string? Title { set; get; }
        public SerieRecord?[]? Series { set; get; }
        public AxisRecord? XAxis { set; get; }
        public AxisRecord? YAxis { set; get; }
        public AxisRecord? ZAxis { set; get; }
        public string[]? BackgroundColor { set; get; }
        public string[]? BorderColor { set; get; }
    }
}