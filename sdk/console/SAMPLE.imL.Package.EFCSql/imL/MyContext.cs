using imL.Contract.DB;
using imL.Package.EFCSql;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;

namespace SAMPLE.imL.Package.EFCSql
{
    public class MyContext : DbContext, IContext
    {
        private readonly string? _CONN;
        private IConnection _CONNECTION;
        private IHelperAsync _HELPER_ASYNC;

        public IConnection Connection
        {
            get
            {
                if (this._CONNECTION == null)
                    this._CONNECTION = new SqlConnectionDefault(this.Database.GetDbConnection());

                return this._CONNECTION;
            }
        }
        public IHelperAsync Helper
        {
            get
            {
                if (this._HELPER_ASYNC == null)
                    this._HELPER_ASYNC = new SqlHelperAsync(this.Connection);

                return this._HELPER_ASYNC;
            }
        }

        public DbSet<MyTableSchema> MyTable { set; get; }

        public MyContext(string _conn)
        {
            this._CONN = _conn;
        }
        public MyContext(DbContextOptions<MyContext> _co) : base(_co)
        {
            SqlServerOptionsExtension _ext = _co.FindExtension<SqlServerOptionsExtension>();

            if (_ext != null)
                this._CONN = _ext.ConnectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder _cob)
        {
            _cob.UseSqlServer(this._CONN);
            _cob.AddInterceptors(
                new TableHintCommandInterceptor(),
                new NoTableCommandInterceptor()
                );
        }

        public DateTime CURRENT_TIMESTAMP() => throw new NotSupportedException();
        public DateTime GETDATE() => throw new NotSupportedException();
        public decimal? ABS(decimal? numeric_expression) => throw new NotSupportedException();
        //public bool BETWEEN(DateTime? expression, DateTime begin_expression, DateTime end_expression) => throw new NotSupportedException();
        protected override void OnModelCreating(ModelBuilder _mb)
        {
            _mb.AddSqlFunctions(this.GetType());
        }
    }
}
