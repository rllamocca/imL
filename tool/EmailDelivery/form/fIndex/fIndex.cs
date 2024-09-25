using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text.Json;
using System.Windows.Forms;

using imL;
using imL.Package.NPOI;

using NPOI.SS.Formula.Functions;

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

    async void button1_Click(object sender, EventArgs e)
    {
        button1.Enabled = false;
        dataGridView1.Rows.Clear();

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

            MailMessageFormat? _basic_mail= _sf.MailMessageFormatBasic;
            SmtpFormat? _basic_smtp = _sf.SmtpFormatBasic;

            if (_basic_smtp == null)
                throw new ArgumentNullException(nameof(_basic_smtp));

            eMailRaw[] _raws = NPOIHelper.LoadGenerics<eMailRaw>(_path, _xls: false);
            IList<eMailPrepared> _prepareds = [];

            foreach (eMailRaw _item in _raws)
            {
                try
                {
                    eMailPrepared _add = new();

                    _add.C00 = Convert.ToString(_item.C00);
                    _add.C01 = Convert.ToString(_item.C01);
                    _add.C02 = Convert.ToString(_item.C02);
                    _add.C03 = Convert.ToString(_item.C03);
                    _add.C04 = Convert.ToString(_item.C04);
                    _add.C05 = Convert.ToString(_item.C05);
                    _add.C06 = Convert.ToString(_item.C06);

                    _add.C07n = [
                        Convert.ToString(_item.C07),
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
                        Convert.ToString(_item.C21),
                        Convert.ToString(_item.C22),
                        Convert.ToString(_item.C23),
                        Convert.ToString(_item.C24),
                        Convert.ToString(_item.C25),
                        Convert.ToString(_item.C26),
                    ];

                    _add.C07n = (
                        from _r in _add.C07n
                        where _r != null
                        select _r
                        ).ToArray();

                    _prepareds.Add(_add);
                }
                catch (Exception _ex)
                {
                    MessageBox.Show(_ex.Message);
                }
            }

            IList<eMailCooked> _cookeds = [];

            foreach (eMailPrepared _item in _prepareds)
            {
                try
                {
                    eMailCooked _add = new();

                    _add.Code = _item.C00;
                    _add.Send = (_item.C01 == "SI");
                    _add.To = _item.C02?.Split(_sf.SplitSeparator);
                    _add.CC = _item.C03?.Split(_sf.SplitSeparator);
                    _add.BCC = _item.C04?.Split(_sf.SplitSeparator);
                    _add.Subject = _item.C05;
                    _add.Body = _item.C06;
                    _add.PathAttachments = _item.C07n;

                    _add.Code = _add.Code?.Trim();
                    //_add.ENVIAR
                    _add.To = AlgunNombreParaAlgunMetodo(_add.To);
                    _add.CC = AlgunNombreParaAlgunMetodo(_add.CC);
                    _add.BCC = AlgunNombreParaAlgunMetodo(_add.BCC);
                    _add.Subject = _add.Subject?.Trim();
                    _add.Body = _add.Body?.Trim();
                    _add.PathAttachments = AlgunNombreParaAlgunMetodo(_add.PathAttachments);

                    _cookeds.Add(_add);

                    int _index = dataGridView1.Rows.Add(_add.Code, _add.Send, _add.Subject, _add.Result);
                    DataGridViewRow _row = dataGridView1.Rows[_index];
                    _row.Tag = _add;
                }
                catch (Exception _ex)
                {
                    MessageBox.Show(_ex.Message);
                }
            }

            using (SmtpClient _using = SmtpHelper.InitSmtpClient(new SmtpClient(), _basic_smtp))
            {
                foreach (eMailCooked _item in _cookeds)
                {
                    try
                    {
                        if ((_item.Send == true) == false)
                            continue;

                        MailMessageFormat _tmp = new MailMessageFormat();

                        _tmp.IsBodyHtml = true;

                        _tmp.FromAddress = _basic_mail?.FromAddress;
                        _tmp.FromDisplayName = _basic_mail?.FromDisplayName;

                        _tmp.TO = _item.To;
                        _tmp.CC = _item.CC;
                        _tmp.BCC = _item.BCC;

                        _tmp.PathAttachments = _item.PathAttachments;

                        _tmp.Subject = _item.Subject;
                        _tmp.Body = _item.Body;

                        _tmp.FromAddress = _tmp.FromAddress ?? _basic_smtp.UserName;
                        //_tmp.FromDisplayName = _tmp.FromDisplayName ?? _basic_smtp.UserName;
                        _tmp.FromDisplayName = "EL PELUCA";

                        using (MailMessage _using2 = SmtpHelper.InitMailMessage(new MailMessage(), _tmp))
                        {
                            await _using.SendMailAsync(_using2);
                            _item.Result = true;
                        }
                    }
                    catch (Exception _ex)
                    {
                        _item.Result = _ex;
                        MessageBox.Show(_ex.Message);
                    }
                }

                dataGridView1.Refresh();
            }

            MessageBox.Show("Procesar");
        }
        catch (Exception _ex)
        {
            MessageBox.Show(_ex.Message);
        }

        button1.Enabled = true;
    }

    static string?[]? AlgunNombreParaAlgunMetodo(string?[]? _array)
    {
        if (_array == null)
            return null;

        return (
            from _r in _array
            where string.IsNullOrWhiteSpace(_r) == false
            select _r.Trim()
            ).ToArray();
    }

    void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || e.ColumnIndex < 0)
            return;

        //MessageBox.Show(string.Format("HI: {0} {1}", e.RowIndex, e.ColumnIndex));

        if (e.ColumnIndex == 0)
        {
            DataGridViewRow _row = dataGridView1.Rows[e.RowIndex];

            if (_row.Tag == null)
                return;

            eMailCooked _tag = (eMailCooked)_row.Tag;

            using feMail _using = new(_tag);
            _using.ShowDialog();
        }
    }
}
