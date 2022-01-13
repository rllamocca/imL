// See https://aka.ms/new-console-template for more information

using imL.Pattern;

using SAMPLE.imL.Utility.Sql;

Console.WriteLine("Hello, World!");

TableXXX _row = new TableXXX()
{
    Pk = 1,
    Fk = 2,
    Name = "row"
};

string _sql = SqlPattern.Insert("_table", _row.GetInsert(), true);

Console.WriteLine(_sql);

Console.ReadKey();