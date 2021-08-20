#if (NET35 || NET40) == false
using System.Threading;
using System.Threading.Tasks;
#endif

using System;

namespace imL.Contract
{
    public interface IConnection : IDisposable
    {
        int TimeOut { set; get; }
        bool Constraints { set; get; }
#if (NET35 || NET40) == false
        CancellationToken Token { set; get; }
#endif

        void Open();
        void Close();

#if (NET35 || NET40) == false
        Task OpenAsync();
#endif
    }
}
