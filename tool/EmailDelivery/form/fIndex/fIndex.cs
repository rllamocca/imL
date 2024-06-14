using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Forms;

using imL;
using imL.Package.NPOI;

namespace EmailDelivery;

public partial class fIndex : Form
{
    public fIndex()
    {
        InitializeComponent();
    }

    void List11_ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using fSetting _using = new();
        _using.ShowDialog();
    }

    void button1_Click(object sender, EventArgs e)
    {
        try
        {
            openFileDialog1.ShowDialog();
            string? _path = openFileDialog1.FileName;

            if (_path == null)
                return;

            textBox1.Text = _path;
            SettingFormat? _sf = JsonSerializer.Deserialize<SettingFormat>(File.ReadAllText(AppLock.PathJson));

            if (_sf == null)
                throw new ArgumentNullException(nameof(_sf));

            SmtpFormat? _op1 = _sf.Option_1;

            if (_op1 == null)
                throw new ArgumentNullException(nameof(_op1));

            eMailRaw[] _raws = NPOIHelper.LoadGenerics<eMailRaw>(_path, _xls: false);
            IList<eMailPrepared> _prepareds = [];

            foreach (eMailRaw _item in _raws)
            {
                try
                {
                    eMailPrepared _add = new();

                    _add.CODIGO = Convert.ToString(_item.C01);
                    _add.ENVIAR = Convert.ToString(_item.C02);
                    _add.PARA = Convert.ToString(_item.C03);
                    _add.CC = Convert.ToString(_item.C04);
                    _add.CCO = Convert.ToString(_item.C05);
                    _add.ASUNTO = Convert.ToString(_item.C06);
                    _add.CUERPO = Convert.ToString(_item.C07);

                    _add.ADJUNTO = [
                        Convert.ToString(_item.C08),
                        Convert.ToString(_item.C09),
                        Convert.ToString(_item.C10),
                        Convert.ToString(_item.C11),
                        Convert.ToString(_item.C12),
                        Convert.ToString(_item.C13),
                        Convert.ToString(_item.C14),
                        Convert.ToString(_item.C15),
                        Convert.ToString(_item.C16),
                        Convert.ToString(_item.C17),
                        Convert.ToString(_item.C18),
                        Convert.ToString(_item.C19),
                        Convert.ToString(_item.C20),
                    ];

                    _add.ADJUNTO = (
                        from _r in _add.ADJUNTO
                        where _r != null
                        select _r
                        ).ToArray();

                    _prepareds.Add(_add);
                }
                catch (Exception _ex)
                {
                }
            }

            IList<eMailCooked> _cookeds = [];

            foreach (eMailPrepared _item in _prepareds)
            {
                try
                {
                    eMailCooked _add = new();

                    _add.CODIGO = _item.CODIGO;
                    _add.ENVIAR = (_item.ENVIAR == "SI");
                    _add.PARA = _item.PARA?.Split(_sf.SplitSeparator);
                    _add.CC = _item.CC?.Split(_sf.SplitSeparator);
                    _add.CCO = _item.CCO?.Split(_sf.SplitSeparator);
                    _add.ASUNTO = _item.ASUNTO;
                    _add.CUERPO = _item.CUERPO;
                    _add.ADJUNTO = _item.ADJUNTO;

                    _add.CODIGO = _add.CODIGO?.Trim();
                    //_add.ENVIAR
                    _add.PARA = (
                        from _r in _add.PARA
                        where _r != null
                        select _r.Trim()
                        ).ToArray()
                    _add.ADJUNTO = (
                        from _r in _add.ADJUNTO
                        where _r != null
                        select _r.Trim()
                        ).ToArray();

                    _cookeds.Add(_add);
                }
                catch (Exception _ex)
                {
                }
            }

        }
        catch (Exception _ex)
        {
            MessageBox.Show(_ex.Message);
        }
    }

    static string?[]? AlgunNombreParaAlgunMetodo(string?[]? _array)
    {
        if (_array == null)
            return null;

        return (
            from _r in _array
            where _r != null
            && _r.Trim().Length > 0
            select _r.Trim()
            ).ToArray();
    }
}
