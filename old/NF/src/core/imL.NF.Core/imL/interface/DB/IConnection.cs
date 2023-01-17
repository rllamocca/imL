#if (NET35 || NET40) == false
using System.Threading;
using System.Threading.Tasks;
#endif

using System;

namespace imL.DB
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
        void Refresh();

#if (NET35 || NET40) == false
        Task OpenAsync();
#endif
#if (NETSTANDARD2_1_OR_GREATER || NET5_0_OR_GREATER)
        Task CloseAsync();
#endif
#if (NET35 || NET40) == false
        Task RefreshAsync();
#endif

    }
}
