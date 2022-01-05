using System.Collections.Generic;

namespace imL.JavaScript.ChartJS
{
    public class ChartFormat
    {
        public string Title { set; get; }
        public IEnumerable<SerieFormat> Series { set; get; }
        public AxisFormat XAxis { set; get; }
        public AxisFormat YAxis { set; get; }
        public AxisFormat ZAxis { set; get; }
        public IEnumerable<string> BackgroundColor { set; get; }
        public IEnumerable<string> BorderColor { set; get; }
    }
}