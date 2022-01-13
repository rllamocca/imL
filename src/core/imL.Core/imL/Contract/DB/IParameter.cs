namespace imL.Contract.DB
{
    public interface IParameter
    {
        string Affect { get; }
        string Expression { get; }
        bool SkipEffect { get; }

        object GetValue();
    }
}
