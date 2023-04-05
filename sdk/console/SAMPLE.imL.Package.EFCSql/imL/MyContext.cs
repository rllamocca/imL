using imL.DB;
using imL.Package.EFCSql;

using Microsoft.EntityFrameworkCore;

namespace SAMPLE.imL.Package.EFCSql
{
    public class MyContext : DbContext, IContext
    {
        private readonly string? _CONN;
        private IConnection _CONNECTION ;
        private IHelper _HELPER_ASYNC;

        public IConnection Connection
        {
            get
            {
                if (this._CONNECTION == null)
                    this._CONNECTION = new SqlConnectionDefault(this.Database.GetDbConnection());

                return this._CONNECTION;
            }
        }
        public IHelper Helper
        {
            get
            {
                if (this._HELPER_ASYNC == null)
                    this._HELPER_ASYNC = new SqlHelper(this.Connection);

                return this._HELPER_ASYNC;
            }
        }

        public DbSet<MyTableSchema> MyTable { set; get; }

        public MyContext(string _conn)
        {
            this._CONN = _conn;
        }
        public MyContext(DbContextOptions<MyContext> _co)
            : base(_co)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder _cob)
        {
            if (_cob.IsConfigured == false)
                _cob.UseSqlServer(_CONN);

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
