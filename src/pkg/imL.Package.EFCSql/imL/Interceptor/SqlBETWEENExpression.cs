//using System.Diagnostics.CodeAnalysis;
//using System.Linq.Expressions;

//using Microsoft.EntityFrameworkCore.Query;
//using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
//using Microsoft.EntityFrameworkCore.Storage;

//namespace imL.Package.EFCSql
//{
//    public class SqlBETWEENExpression : SqlExpression
//    {
//        public SqlBETWEENExpression(
//            string functionName,
//            bool nullable,
//            Type type,
//            RelationalTypeMapping? typeMapping)
//            : this(
//                instance: null, schema: null, functionName, nullable, instancePropagatesNullability: null, builtIn: true, type, typeMapping)
//        {
//        }

//        public SqlBETWEENExpression(
//            string schema,
//            string functionName,
//            bool nullable,
//            Type type,
//            RelationalTypeMapping? typeMapping)
//            : this(
//                instance: null, schema, functionName, nullable, instancePropagatesNullability: null,
//                builtIn: false, type, typeMapping)
//        {
//        }

//        public SqlBETWEENExpression(
//            SqlExpression instance,
//            string functionName,
//            bool nullable,
//            bool instancePropagatesNullability,
//            Type type,
//            RelationalTypeMapping? typeMapping)
//            : this(
//                instance, schema: null, functionName, nullable, instancePropagatesNullability,
//                builtIn: true, type, typeMapping)
//        {
//        }

//        private SqlBETWEENExpression(
//            SqlExpression? instance,
//            string? schema,
//            string name,
//            bool nullable,
//            bool? instancePropagatesNullability,
//            bool builtIn,
//            Type type,
//            RelationalTypeMapping? typeMapping)
//            : this(
//                instance, schema, name, niladic: true, arguments: null, nullable, instancePropagatesNullability,
//                argumentsPropagateNullability: null, builtIn, type, typeMapping)
//        {
//        }

//        public SqlBETWEENExpression(
//            string functionName,
//            IEnumerable<SqlExpression> arguments,
//            bool nullable,
//            IEnumerable<bool> argumentsPropagateNullability,
//            Type type,
//            RelationalTypeMapping? typeMapping)
//            : this(
//                instance: null, schema: null, functionName, arguments, nullable, instancePropagatesNullability: null,
//                argumentsPropagateNullability, builtIn: true, type, typeMapping)
//        {
//        }

//        public SqlBETWEENExpression(
//            string? schema,
//            string functionName,
//            IEnumerable<SqlExpression> arguments,
//            bool nullable,
//            IEnumerable<bool> argumentsPropagateNullability,
//            Type type,
//            RelationalTypeMapping? typeMapping)
//            : this(
//                instance: null, schema, functionName, arguments, nullable,
//                instancePropagatesNullability: null, argumentsPropagateNullability, builtIn: false, type, typeMapping)
//        {
//        }

//        public SqlBETWEENExpression(
//            SqlExpression instance,
//            string functionName,
//            IEnumerable<SqlExpression> arguments,
//            bool nullable,
//            bool instancePropagatesNullability,
//            IEnumerable<bool> argumentsPropagateNullability,
//            Type type,
//            RelationalTypeMapping? typeMapping)
//            : this(
//                instance, schema: null, functionName, arguments, nullable, instancePropagatesNullability,
//                argumentsPropagateNullability, builtIn: true, type, typeMapping)
//        {
//        }

//        private SqlBETWEENExpression(
//            SqlExpression? instance,
//            string? schema,
//            string name,
//            IEnumerable<SqlExpression> arguments,
//            bool nullable,
//            bool? instancePropagatesNullability,
//            IEnumerable<bool> argumentsPropagateNullability,
//            bool builtIn,
//            Type type,
//            RelationalTypeMapping? typeMapping)
//            : this(
//                instance, schema, name, niladic: false, arguments, nullable,
//                instancePropagatesNullability, argumentsPropagateNullability, builtIn,
//                type, typeMapping)
//        {
//        }

//        private SqlBETWEENExpression(
//            SqlExpression? instance,
//            string? schema,
//            string name,
//            bool niladic,
//            IEnumerable<SqlExpression>? arguments,
//            bool nullable,
//            bool? instancePropagatesNullability,
//            IEnumerable<bool>? argumentsPropagateNullability,
//            bool builtIn,
//            Type type,
//            RelationalTypeMapping? typeMapping)
//            : base(type, typeMapping)
//        {
//            Instance = instance;
//            Name = name;
//            Schema = schema;
//            IsNiladic = niladic;
//            IsBuiltIn = builtIn;
//            Arguments = arguments?.ToList();
//            IsNullable = nullable;
//            InstancePropagatesNullability = instancePropagatesNullability;
//            ArgumentsPropagateNullability = argumentsPropagateNullability?.ToList();
//        }

//        public virtual string Name { get; }
//        public virtual string? Schema { get; }
//        [MemberNotNullWhen(false, nameof(Arguments), nameof(ArgumentsPropagateNullability))]
//        public virtual bool IsNiladic { get; }
//        public virtual bool IsBuiltIn { get; }
//        public virtual IReadOnlyList<SqlExpression>? Arguments { get; }
//        public virtual SqlExpression? Instance { get; }
//        public virtual bool IsNullable { get; }
//        public virtual bool? InstancePropagatesNullability { get; }
//        public virtual IReadOnlyList<bool>? ArgumentsPropagateNullability { get; }

//        protected override Expression VisitChildren(ExpressionVisitor visitor)
//        {
//            var changed = false;
//            var instance = (SqlExpression?)visitor.Visit(Instance);
//            changed |= instance != Instance;

//            SqlExpression[]? arguments = default;
//            if (!IsNiladic)
//            {
//                arguments = new SqlExpression[Arguments.Count];
//                for (var i = 0; i < arguments.Length; i++)
//                {
//                    arguments[i] = (SqlExpression)visitor.Visit(Arguments[i]);
//                    changed |= arguments[i] != Arguments[i];
//                }
//            }

//            return changed
//                ? new SqlBETWEENExpression(
//                    instance,
//                    Schema,
//                    Name,
//                    IsNiladic,
//                    arguments,
//                    IsNullable,
//                    InstancePropagatesNullability,
//                    ArgumentsPropagateNullability,
//                    IsBuiltIn,
//                    Type,
//                    TypeMapping)
//                : this;
//        }
//        public virtual SqlBETWEENExpression ApplyTypeMapping(RelationalTypeMapping? typeMapping)
//            => new(
//                Instance,
//                Schema,
//                Name,
//                IsNiladic,
//                Arguments,
//                IsNullable,
//                InstancePropagatesNullability,
//                ArgumentsPropagateNullability,
//                IsBuiltIn,
//                Type,
//                typeMapping ?? TypeMapping);
//        public virtual SqlBETWEENExpression Update(SqlExpression? instance, IReadOnlyList<SqlExpression>? arguments)
//            => instance != Instance || (arguments != null && Arguments != null && !arguments.SequenceEqual(Arguments))
//                ? new SqlBETWEENExpression(
//                    instance,
//                    Schema,
//                    Name,
//                    IsNiladic,
//                    arguments,
//                    IsNullable,
//                    InstancePropagatesNullability,
//                    ArgumentsPropagateNullability,
//                    IsBuiltIn,
//                    Type,
//                    TypeMapping)
//                : this;
//        protected override void Print(ExpressionPrinter expressionPrinter)
//        {
//            if (!string.IsNullOrEmpty(Schema))
//            {
//                expressionPrinter.Append(Schema).Append(".").Append(Name);
//            }
//            else
//            {
//                if (Instance != null)
//                {
//                    expressionPrinter.Visit(Instance);
//                    expressionPrinter.Append(".");
//                }

//                expressionPrinter.Append(Name);
//            }

//            if (!IsNiladic)
//            {
//                expressionPrinter.Append(" ");
//                expressionPrinter.VisitCollection(Arguments);
//                expressionPrinter.Append(" ");
//            }
//        }
//        public override bool Equals(object? obj)
//            => obj != null
//                && (ReferenceEquals(this, obj)
//                    || obj is SqlBETWEENExpression sqlFunctionExpression
//                    && Equals(sqlFunctionExpression));
//        private bool Equals(SqlBETWEENExpression sqlFunctionExpression)
//            => base.Equals(sqlFunctionExpression)
//                && IsNiladic == sqlFunctionExpression.IsNiladic
//                && Name == sqlFunctionExpression.Name
//                && Schema == sqlFunctionExpression.Schema
//                && ((Instance == null && sqlFunctionExpression.Instance == null)
//                    || (Instance != null && Instance.Equals(sqlFunctionExpression.Instance)))
//                && ((Arguments == null && sqlFunctionExpression.Arguments == null)
//                    || (Arguments != null
//                        && sqlFunctionExpression.Arguments != null
//                        && Arguments.SequenceEqual(sqlFunctionExpression.Arguments)));
//        public override int GetHashCode()
//        {
//            var hash = new HashCode();
//            hash.Add(base.GetHashCode());
//            hash.Add(Name);
//            hash.Add(IsNiladic);
//            hash.Add(Schema);
//            hash.Add(Instance);

//            if (Arguments != null)
//            {
//                for (var i = 0; i < Arguments.Count; i++)
//                {
//                    hash.Add(Arguments[i]);
//                }
//            }

//            return hash.ToHashCode();
//        }
//    }
//}
