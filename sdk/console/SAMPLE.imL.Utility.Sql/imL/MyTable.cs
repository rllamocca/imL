using System.Data;

using imL.Contract.DB;
using imL.Utility.Sql;

namespace SAMPLE.imL.Utility.Sql
{
    internal class MyTable
    {
        public ulong? Pk { set; get; }
        public uint? Fk { set; get; }
        public DateTime? Created { set; get; }
        public string? Name { set; get; }
        public string? Function { set; get; }

        public IParameter[] GetSelect()
        {
            return new IParameter[] {
                new SqlParameterDefault("Pk", this.Pk, SqlDbType.BigInt, true),
                new SqlParameterDefault("Fk", this.Fk, SqlDbType.Int, true)
            };
        }

        public IParameter[] GetInsert()
        {
            return new IParameter[] {
                new SqlParameterDefault("Pk", this.Pk, SqlDbType.BigInt),
                new ParameterDefault("Function", "CONVERT(NVARCHAR(16),@Pk + @Fk) + @Name + @Function + 'SQL'"),
                new SqlParameterDefault("Fk", this.Fk, SqlDbType.Int),
                new ParameterDefault("Created", "GETDATE()"),
                new SqlParameterDefault("Name", this.Name, SqlDbType.NVarChar, 32),
                new SqlParameterDefault("Function", "VALOR", SqlDbType.NVarChar, 16),
                new ParameterDefault("Function", "+'FUNCTION'")
            };
        }

        public IParameter[] GetUpdate()
        {
            return new IParameter[] {
                new SqlParameterDefault("Pk", this.Pk, SqlDbType.BigInt, true),
                new ParameterDefault("Function", "CONVERT(NVARCHAR(16),@Pk + @Fk) + @Name + @Function + 'SQL'"),
                new SqlParameterDefault("Fk", this.Fk, SqlDbType.Int, true),
                new ParameterDefault("Created", "GETDATE()", true),
                new SqlParameterDefault("Name", this.Name, SqlDbType.NVarChar, 32),
                new SqlParameterDefault("Function", "VALOR", SqlDbType.NVarChar, 16),
                new ParameterDefault("Function", "+'FUNCTION'")
            };
        }
        public IParameter[] GetDelete()
        {
            return new IParameter[] {
                new SqlParameterDefault("Pk1", this.Pk, SqlDbType.BigInt, true),
                new SqlParameterDefault("Fk2", this.Fk, SqlDbType.Int, true)
            };
        }
    }
}
