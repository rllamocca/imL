using System;
using System.Threading;

namespace imL.Contract
{
    public interface IPeriodExecution : IDisposable
    {
        long Count { get; }
        IAppInfo App { get; }
        CancellationToken Token { get; }
        DateTime Start { get; }
        string Guid { get; }

        void PopulateWithSomething(long _count, IAppInfo _app, CancellationToken _token = default);

        void AfterPopulate();
        string WorkingDoInfo();
        string ToString();
    }
}
