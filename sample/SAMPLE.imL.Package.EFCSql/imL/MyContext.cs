using imL.Contract.DB;
using imL.Package.EFCSql;

using Microsoft.EntityFrameworkCore;

namespace SAMPLE.imL.Package.EFCSql
{
    public class MyContext : DbContext, IContext
    {
        private readonly string _CONN;
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
        protected override void OnConfiguring(DbContextOptionsBuilder _ob)
        {
            _ob.UseSqlServer(this._CONN);
        }
    }
}
