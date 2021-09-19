namespace imL.JavaScript.ChartJS
{
    public class SerieFormat
    {
        public int?[] Values { set; get; }
        public string Name { set; get; }
        public string BackgroundColor { set; get; }
        public string BorderColor { set; get; }

        public SerieFormat(int?[] _values, string _name = null, string _backgroundcolor = null, string _bordercolor = null)
        {
            this.Values = _values;
            this.Name = _name;
            this.BackgroundColor = _backgroundcolor;
            this.BorderColor = _bordercolor;
        }
    }
}