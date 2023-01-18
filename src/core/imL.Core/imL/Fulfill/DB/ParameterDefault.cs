namespace imL.Contract.DB
{
    public class ParameterDefault : IParameter
    {
        public string Affect { get; }
        public string Expression { get; }
        public bool IsSearchCondition { get; }

        public ParameterDefault(
            string _affect,
            string _expression = null,
            bool _issearch = false
            )
        {
            Affect = _affect;
            Expression = _expression;
            IsSearchCondition = _issearch;
        }

        public object GetValue()
        {
            return null;
        }
    }
}