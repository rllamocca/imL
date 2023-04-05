using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace imL.Package.EFCSql
{
    public static class DbContextExtension
    {
        public static async Task ThrowCanConnectAsync(this DbContext? _this, CancellationToken _ct = default)
        {
            if (_this == null)
                return;

            if (false == await _this.Database.CanConnectAsync(_ct))
                throw new AccessViolationException(nameof(DatabaseFacade.CanConnectAsync));
        }
    }
}
