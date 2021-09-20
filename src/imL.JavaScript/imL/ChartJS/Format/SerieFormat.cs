namespace imL.JavaScript.ChartJS
{
    public class SerieFormat
    {
        public int?[] Values { set; get; }
        public string Name { set; get; }

        public SerieFormat(int?[] _values, string _name = null)
        {
            this.Values = _values;
            this.Name = _name;
        }
    }
}