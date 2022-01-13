namespace imL.Contract.DB
{
    public class ParameterDefault : IParameter
    {
        public string Affect { get; }
        public string Expression { get; }
        public bool SkipEffect { get; }

        public ParameterDefault(
            string _affect,
            string _expression
            )
        {
            this.Affect = _affect;
            this.Expression = _expression;
        }

        public object GetValue()
        {
            return null;
        }
    }
}