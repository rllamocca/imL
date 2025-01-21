using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace imL.Package.EFCSql
{
    public static class ModelBuilderExtension
    {
        public static ModelBuilder AddSqlFunctions(this ModelBuilder _this, Type _type)
        {
            _this.HasDbFunction(_type.GetMethod("CURRENT_TIMESTAMP"))
                .HasTranslation(_b =>
                {
                    return new SqlFunctionExpression(
                        "CURRENT_TIMESTAMP",
                        nullable: false,
                        typeof(DateTime),
                        null
                        );
                });

            _this.HasDbFunction(_type.GetMethod("GETDATE"))
                .HasTranslation(_b =>
                {
                    return new SqlFunctionExpression(
                        "GETDATE()",
                        nullable: false,
                        typeof(DateTime),
                        null
                        );
                });

            //ABS ( numeric_expression )  
            _this.HasDbFunction(_type.GetMethod("ABS"))
                .HasTranslation(_b =>
                new SqlFunctionExpression(
                    "ABS",
                    _b,
                    nullable: true,
                    argumentsPropagateNullability: new[] { true },
                    type: _b.First().Type,
                    typeMapping: _b.First().TypeMapping)
                );

            ////expression[NOT] BETWEEN begin_expression AND end_expression
            //_this.HasDbFunction(_type.GetMethod("BETWEEN"))
            //    .HasTranslation(_b =>
            //    new SqlBETWEENExpression(
            //        "BETWEEN",
            //        _b,
            //        nullable: true,
            //        argumentsPropagateNullability: new[] { true, true },
            //        type: _b.First().Type,
            //        typeMapping: _b.First().TypeMapping)
            //    );

            return _this;
        }
    }
}
