namespace imL.DB
{
    public interface IParameter
    {
        string Affect { get; }
        string Expression { get; }
        bool? IsSearchCondition { get; }

        object GetValue();
    }
}
