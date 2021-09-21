namespace imL.JavaScript.ChartJS
{
    public class SerieFormat
    {
        public decimal?[] Values { set; get; }
        public string Name { set; get; }
        public string Stack { set; get; }

        public SerieFormat(decimal?[] _values, string _name = null, string _stack = null)
        {
            this.Values = _values;
            this.Name = _name;
            this.Stack = _stack;
        }
    }
}