// See https://aka.ms/new-console-template for more information

using imL.Pattern;

using SAMPLE.imL.Utility.Sql;

MyTable _row = new()
{
    Pk = 1,
    Fk = 2,
    Name = "row"
};

string _sql;
_sql = SqlPattern.Select("MyTable", _row.GetSelect()); Console.WriteLine(_sql);
_sql = SqlPattern.Insert("MyTable", _row.GetInsert(), true); Console.WriteLine(_sql);
_sql = SqlPattern.Update("MyTable", _row.GetUpdate()); Console.WriteLine(_sql);
_sql = SqlPattern.Delete("MyTable", _row.GetDelete()); Console.WriteLine(_sql);

Console.ReadKey();