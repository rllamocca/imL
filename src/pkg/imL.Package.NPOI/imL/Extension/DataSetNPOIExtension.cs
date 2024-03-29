﻿using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

using System;
using System.Data;
using System.IO;
using System.Linq;

namespace imL.Package.NPOI
{
    public static class DataSetNPOIExtension
    {
        public static void ExcelCreate(this DataSet _this,
            string _path,
            bool _columnnames = true,
            bool _xls = true,
            IProgress<int> _dt_progress = null,
            IProgress<int> _progress = null)
        {
            _this.GetExcel(out Stream _out, _columnnames, _xls, _dt_progress, _progress);
            _out.CheckBeginPosition();
            _out.FileCreate(_path);
            _out.Dispose();
        }
        public static void GetExcel(this DataSet _this,
            out Stream _out,
            bool _columnnames = true,
            bool _xls = true,
            IProgress<int> _dt_progress = null,
            IProgress<int> _progress = null)
        {
            IWorkbook _wb;
            if (_xls)
                _wb = new HSSFWorkbook();
            else
                _wb = new XSSFWorkbook();

            IFont _fbasic = _wb.CreateFont();
            _fbasic.FontHeightInPoints = 10;
            _fbasic.FontName = "Consolas";

            IFont _fbold = _wb.CreateFont();
            _fbold.FontHeightInPoints = 12;
            _fbold.FontName = "Consolas";
            _fbold.IsBold = true;

            ICellStyle _sbasic = _wb.CreateCellStyle();
            _sbasic.Alignment = HorizontalAlignment.Left;
            _sbasic.VerticalAlignment = VerticalAlignment.Center;
            _sbasic.SetFont(_fbasic);

            ICellStyle _sbasic_number = _wb.CreateCellStyle();
            _sbasic_number.Alignment = HorizontalAlignment.Right;
            _sbasic_number.VerticalAlignment = VerticalAlignment.Center;
            _sbasic_number.SetFont(_fbasic);

            ICellStyle _sbold = _wb.CreateCellStyle();
            _sbold.Alignment = HorizontalAlignment.Center;
            _sbold.VerticalAlignment = VerticalAlignment.Center;
            _sbold.SetFont(_fbold);

            ICellStyle _stime = _wb.CreateCellStyle();
            _stime.Alignment = HorizontalAlignment.Right;
            _stime.VerticalAlignment = VerticalAlignment.Center;
            _stime.DataFormat = _wb.CreateDataFormat().GetFormat("H:mm:ss");
            _stime.SetFont(_fbasic);

            ICellStyle _stime_acum = _wb.CreateCellStyle();
            _stime_acum.Alignment = HorizontalAlignment.Right;
            _stime_acum.VerticalAlignment = VerticalAlignment.Center;
            _stime_acum.DataFormat = _wb.CreateDataFormat().GetFormat("[H]:mm:ss");
            _stime_acum.SetFont(_fbasic);

            ICellStyle _sdate = _wb.CreateCellStyle();
            _sdate.Alignment = HorizontalAlignment.Right;
            _sdate.VerticalAlignment = VerticalAlignment.Center;
            _sdate.DataFormat = _wb.CreateDataFormat().GetFormat("yyyyMMdd");
            _sdate.SetFont(_fbasic);

            ICellStyle _sdatetime = _wb.CreateCellStyle();
            _sdatetime.Alignment = HorizontalAlignment.Right;
            _sdatetime.VerticalAlignment = VerticalAlignment.Center;
            _sdatetime.DataFormat = _wb.CreateDataFormat().GetFormat("yyyyMMdd H:mm:ss");
            _sdatetime.SetFont(_fbasic);

            foreach (DataTable _item in _this.Tables)
            {
                int _row = 0;
                int _col = 0;

                ISheet _wbs = _wb.CreateSheet(_item.TableName);
                _wbs.DefaultColumnWidth = 17;
                IRow _wbsr;

                if (_columnnames)
                {
                    _wbsr = _wbs.CreateRow(_row);
                    foreach (DataColumn _item2 in _item.Columns)
                    {
                        ICell _wbsrc = _wbsr.CreateCell(_col);
                        _wbsrc.CellStyle = _sbold;
                        _wbsrc.SetCellType(CellType.String);

                        if (_item2.Caption == null)
                            _wbsrc.SetCellValue(_item2.ColumnName);
                        else
                            _wbsrc.SetCellValue(_item2.Caption);

                        _col++;
                    }
                    _row++;
                }

                foreach (DataRow _item2 in _item.Rows)
                {
                    _col = 0;
                    _wbsr = _wbs.CreateRow(_row);
                    foreach (DataColumn _item3 in _item.Columns)
                    {
                        ICell _wbsrc = _wbsr.CreateCell(_col);
                        object _obj = _item2[_item3.ColumnName];
                        if (_obj != DBNull.Value && _obj != null)
                        {
                            switch (Convert.ToString(_item3.DataType))
                            {
                                case "System.Int16":
                                case "System.Int32":
                                case "System.Int64":
                                case "System.UInt16":
                                case "System.UInt32":
                                case "System.UInt64":
                                case "System.Decimal":
                                    _wbsrc.CellStyle = _sbasic_number;
                                    _wbsrc.SetCellType(CellType.Numeric);
                                    _wbsrc.SetCellValue(Convert.ToDouble(_obj));
                                    break;
                                case "System.DateTime":
                                    DateTime _tmp = Convert.ToDateTime(_obj);

                                    if (_tmp.Excel18991231())
                                    {
                                        _wbsrc.CellStyle = _stime;
                                        _tmp = _tmp.ToExcelTime();
                                        _wbsrc.SetCellValue(_tmp.ToOADate());
                                    }
                                    else
                                    {
                                        if (_tmp.Year == 1900)
                                            _wbsrc.CellStyle = _stime_acum;
                                        else if (_tmp.IsDateTime())
                                            _wbsrc.CellStyle = _sdatetime;
                                        else
                                            _wbsrc.CellStyle = _sdate;

                                        _wbsrc.SetCellValue(_tmp);
                                    }
                                    break;
                                default:
                                    _wbsrc.CellStyle = _sbasic;
                                    _wbsrc.SetCellType(CellType.String);
                                    _wbsrc.SetCellValue(Convert.ToString(_obj));
                                    break;
                            }
                        }
                        _col++;
                    }
                    _row++;

                    _progress?.Report(_row);
                }

                //_col = 0;
                //foreach (DataColumn _dc in _item.Columns)
                //{
                //    _wbs.AutoSizeColumn(_col);
                //    _col++;
                //}

                _dt_progress?.Report(0);
            }

            _out = new MemoryStream();
#if NET35
            _wb.Write(_out);
#else
            if (_wb is XSSFWorkbook _wb2)
                _wb2.Write(_out, true);
            else
            {
#if (NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)
                _wb.Write(_out, true);
#else
                _wb.Write(_out);
#endif
            }
#endif
        }

        public static void ExcelCreate(this DataSet _this,
            string _path,
            Tuple<string, string, string>[] _formats,
            bool _columnnames = true,
            bool _xls = true,
            IProgress<int> _dt_progress = null,
            IProgress<int> _progress = null)
        {
            _this.GetExcel(out Stream _out, _formats, _columnnames, _xls, _dt_progress, _progress);
            _out.CheckBeginPosition();
            _out.FileCreate(_path);
            _out.Dispose();
        }
        public static void GetExcel(this DataSet _this,
            out Stream _out,
            Tuple<string, string, string>[] _formats,
            bool _columnnames = true,
            bool _xls = true,
            IProgress<int> _dt_progress = null,
            IProgress<int> _progress = null)
        {
            IWorkbook _wb;
            if (_xls)
                _wb = new HSSFWorkbook();
            else
                _wb = new XSSFWorkbook();

            IFont _fbasic = _wb.CreateFont();
            _fbasic.FontHeightInPoints = 10;
            _fbasic.FontName = "Consolas";

            IFont _fbold = _wb.CreateFont();
            _fbold.FontHeightInPoints = 12;
            _fbold.FontName = "Consolas";
            _fbold.IsBold = true;

            ICellStyle _sbasic = _wb.CreateCellStyle();
            _sbasic.Alignment = HorizontalAlignment.Left;
            _sbasic.VerticalAlignment = VerticalAlignment.Center;
            _sbasic.SetFont(_fbasic);

            ICellStyle _sbold = _wb.CreateCellStyle();
            _sbold.Alignment = HorizontalAlignment.Center;
            _sbold.VerticalAlignment = VerticalAlignment.Center;
            _sbold.SetFont(_fbold);

            foreach (DataTable _item in _this.Tables)
            {
                int _row = 0;
                int _col = 0;

                ISheet _wbs = _wb.CreateSheet(_item.TableName);
                _wbs.DefaultColumnWidth = 17;
                IRow _wbsr;

                if (_columnnames)
                {
                    _wbsr = _wbs.CreateRow(_row);
                    foreach (DataColumn _item2 in _item.Columns)
                    {
                        ICell _wbsrc = _wbsr.CreateCell(_col);
                        _wbsrc.CellStyle = _sbold;
                        _wbsrc.SetCellType(CellType.String);

                        if (_item2.Caption == null)
                            _wbsrc.SetCellValue(_item2.ColumnName);
                        else
                            _wbsrc.SetCellValue(_item2.Caption);

                        _col++;
                    }
                    _row++;
                }

                _col = 0;
                ICellStyle[] _t_styles = new ICellStyle[_item.Columns.Count];
                foreach (DataColumn _item2 in _item.Columns)
                {
                    Tuple<string, string, string> _f = _formats.Where(_w => _w.Item1 == _item.TableName && _w.Item2 == _item2.ColumnName).FirstOrDefault();

                    if (_f.Equals(default(Tuple<string, string, string>)))
                        _t_styles[_col] = _sbasic;
                    else
                    {
                        _t_styles[_col] = _wb.CreateCellStyle();
                        _t_styles[_col].Alignment = HorizontalAlignment.Left;
                        _t_styles[_col].VerticalAlignment = VerticalAlignment.Center;
                        _t_styles[_col].DataFormat = _wb.CreateDataFormat().GetFormat(_f.Item3);
                        _t_styles[_col].SetFont(_fbasic);
                    }

                    _col++;
                }

                foreach (DataRow _item2 in _item.Rows)
                {
                    _col = 0;
                    _wbsr = _wbs.CreateRow(_row);
                    foreach (DataColumn _item3 in _item.Columns)
                    {
                        ICell _wbsrc = _wbsr.CreateCell(_col);
                        _wbsrc.CellStyle = _t_styles[_col];
                        object _obj = _item2[_item3.ColumnName];
                        if (_obj != DBNull.Value && _obj != null)
                        {
                            switch (Convert.ToString(_item3.DataType))
                            {
                                case "System.Int16":
                                case "System.Int32":
                                case "System.Int64":
                                case "System.UInt16":
                                case "System.UInt32":
                                case "System.UInt64":
                                case "System.Decimal":
                                    _wbsrc.SetCellType(CellType.Numeric);
                                    _wbsrc.SetCellValue(Convert.ToDouble(_obj));
                                    break;
                                case "System.DateTime":
                                    DateTime _tmp = Convert.ToDateTime(_obj);

                                    if (_tmp.Excel18991231())
                                    {
                                        _tmp = _tmp.ToExcelTime();
                                        _wbsrc.SetCellValue(_tmp.ToOADate());
                                    }
                                    else
                                        _wbsrc.SetCellValue(_tmp);
                                    break;
                                default:
                                    _wbsrc.SetCellType(CellType.String);
                                    _wbsrc.SetCellValue(Convert.ToString(_obj));
                                    break;
                            }
                        }
                        _col++;
                    }
                    _row++;

                    _progress?.Report(_row);
                }

                _dt_progress?.Report(0);
            }

            _out = new MemoryStream();
#if NET35
            _wb.Write(_out);
#else
            if (_wb is XSSFWorkbook _wb2)
                _wb2.Write(_out, true);
            else
            {
#if (NETSTANDARD2_0_OR_GREATER || NET5_0_OR_GREATER)
                _wb.Write(_out, true);
#else
                _wb.Write(_out);
#endif
            }
#endif
        }
    }
}
