using imL.Contract.DB;
using imL.Utility.Sql;

using System.Data;

namespace SAMPLE.imL.Utility.Sql
{
    internal class TableXXX
    {
        public ulong? Pk { set; get; }
        public uint? Fk { set; get; }
        public DateTime? Created { set; get; }
        public string? Name { set; get; }
        public string? Function { set; get; }

        public IParameter[] GetInsert()
        {
            return new IParameter[] {
                new SqlParameterDefault("Pk", this.Pk, SqlDbType.BigInt),
                new ParameterDefault("Function", "CONVERT(NVARCHAR(16),@Pk + @Fk) + @Name + @Function + 'SQL'"),
                new SqlParameterDefault("Fk", this.Fk, SqlDbType.Int),
                new ParameterDefault("Created", "GETDATE()"),
                new SqlParameterDefault("Name", this.Name, SqlDbType.NVarChar, 32),
                new SqlParameterDefault("Function", "VALOR", SqlDbType.NVarChar, 16, true),
                new ParameterDefault("Function", "+'FUNCTION'"),
            };
        }
    }
}
