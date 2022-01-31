// See https://aka.ms/new-console-template for more information
using System.Data;

using imL.Package.EFCSql;

using Microsoft.EntityFrameworkCore;

using SAMPLE.imL.Package.EFCSql;

string _conn = @"";

DateTime _a;
DateTime _b;
TimeSpan _ts;
//################################################################################
_a = DateTime.Now;
using (IContext _context = new MyContext(_conn))
    await _context.Connection.RefreshAsync();
_b = DateTime.Now;
_ts = _b - _a;
Console.WriteLine("");
Console.WriteLine("{0} {1} |{2}", _a, _b, _ts);
//################################################################################
_a = DateTime.Now;
using (IContext _context = new MyContext(_conn))
{
    string _sql = @"
SELECT TOP 8 Pk
    ,Date
    ,Value
FROM MyTable
WHERE Deleted IS NULL
AND CONVERT(DATE,Date) BETWEEN @_BEGIN AND @_END
ORDER BY Date, Pk;
";
    await _context.Helper.Connection.OpenAsync();
    MyTableSchema[] _query = await _context.Helper.LoadDataAsync<MyTableSchema>(_sql,
        new SqlParameterDefault("_begin", _a.AddDays(-15), SqlDbType.Date),
        new SqlParameterDefault("_end", _a.AddDays(15), SqlDbType.Date)
        );
    await _context.Helper.Connection.CloseAsync();
    Console.WriteLine("");

    foreach (MyTableSchema _item in _query)
        Console.WriteLine("{0}] {1}| {2}", _item.Pk, _item.Date, _item.Value);
}
_b = DateTime.Now;
_ts = _b - _a;
Console.WriteLine("");
Console.WriteLine("{0} {1} |{2}", _a, _b, _ts);
//################################################################################
_a = DateTime.Now;
using (MyContext _context = new(_conn))
{
    IQueryable<MyTableSchema> _query = from _t in _context.MyTable.AsQueryable()
                                       orderby _t.Pk
                                       select _t;

    _query = (from _t in _query
              where _t.Deleted == null
              where _a.AddDays(-15).Date <= _t.Date && _t.Date <= _a.AddDays(15).Date
              select new MyTableSchema
              {
                  Pk = _t.Pk,
                  Date = _t.Date,
                  Value = _t.Value
              })
              .Take(8);
    Console.WriteLine("");

    foreach (MyTableSchema _item in await _query.ToArrayAsync())
        Console.WriteLine("{0}] {1}| {2}", _item.Pk, _item.Date, _item.Value);
}
_b = DateTime.Now;
_ts = _b - _a;
Console.WriteLine("");
Console.WriteLine("{0} {1} |{2}", _a, _b, _ts);
//################################################################################