using System.Data.Common;
using System.Text.RegularExpressions;

using Microsoft.EntityFrameworkCore.Diagnostics;

namespace imL.Package.EFCSql
{
    public class TableHintCommandInterceptor : DbCommandInterceptor
    {
        private static readonly string _TAG = "-- TABLE_HINT:";
        private static readonly Regex _RE = new(@"(?<table_hint>(?:from|join) +(\[.*\]\.)?(\[.*\]) as (\[.*\])(?! with))",
            RegexOptions.IgnoreCase |
            RegexOptions.Multiline |
            RegexOptions.Compiled
            );

        private static void ManipulateCommand(DbCommand _cmd)
        {
            string _text = _cmd.CommandText;
            string _line = _text
                .Split(new[] { '\r', '\n' })
                .Where(_s => _s.StartsWith(_TAG, StringComparison.Ordinal))
                .FirstOrDefault();

            if (string.IsNullOrWhiteSpace(_line) == false)
            {
                _line = _line?.Replace(_TAG, "").Trim();

                if (string.IsNullOrWhiteSpace(_line) == false)
                    _text = _RE.Replace(_text, "${table_hint} " + string.Format("with ({0})", _line));

                _cmd.CommandText = _text;

#if DEBUG
                Console.WriteLine("{0}>> {1}", nameof(TableHintCommandInterceptor), _cmd.CommandText);
#endif
            }
        }

        //public override InterceptionResult<DbCommand> CommandCreating(
        //    CommandCorrelatedEventData _cced,
        //    InterceptionResult<DbCommand> _return)
        //{
        //    ManipulateCommand(_return.Result);

        //    return _return;
        //}

        //public override DbCommand CommandCreated(
        //    CommandEndEventData _ced,
        //    DbCommand _return)
        //{
        //    ManipulateCommand(_return);

        //    return _return;
        //}

        public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand _c, CommandEventData _ced, InterceptionResult<DbDataReader> _return)
        {
            ManipulateCommand(_c);

            return _return;
        }
        public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand _c, CommandEventData _ced, InterceptionResult<DbDataReader> _return, CancellationToken _ct = default)
        {
            ManipulateCommand(_c);

            return new ValueTask<InterceptionResult<DbDataReader>>(_return);
        }
    }
}
