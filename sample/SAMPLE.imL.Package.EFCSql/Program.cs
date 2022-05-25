// See https://aka.ms/new-console-template for more information
using System.Data;

using imL.Package.EFCSql;
using imL.Struct;

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
    IQueryable<MyTableSchema> _query = from _t in _context.MyTable.AsDefault()
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

    IQueryable<Lot<MyTableSchema, MyTableSchema>> _query2 =
        from _l1 in _context.MyTable.AsDefault()
        join _l2 in _context.MyTable.AsDefault() on _l1.Pk equals _l2.Pk
        select new Lot<MyTableSchema, MyTableSchema>(_l1, _l2);

    Lot<MyTableSchema, MyTableSchema>[] _result = await _query2.ToArrayAsync();

    var _om = OneToMany<MyTableSchema, MyTableSchema>.FromLot(_result, _r => _r.Pk);

    IQueryable<ValueTuple<MyTableSchema, MyTableSchema>> _query3 =
        from _l1 in _context.MyTable.AsDefault()
        join _l2 in _context.MyTable.AsDefault() on _l1.Pk equals _l2.Pk
        select new ValueTuple<MyTableSchema, MyTableSchema>(_l1, _l2);

    ValueTuple<MyTableSchema, MyTableSchema>[] _result2 = await _query3.ToArrayAsync();
}
_b = DateTime.Now;
_ts = _b - _a;
Console.WriteLine("");
Console.WriteLine("{0} {1} |{2}", _a, _b, _ts);
//################################################################################