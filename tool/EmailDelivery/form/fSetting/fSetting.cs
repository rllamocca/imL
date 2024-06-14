using System;
using System.IO;
using System.Net.Mail;
using System.Text.Json;
using System.Windows.Forms;

using imL;

namespace EmailDelivery;

public partial class fSetting : Form
{
    public fSetting()
    {
        InitializeComponent();

        _1_DeliveryMethod.DataSource = Enum.GetValues<SmtpDeliveryMethod>();
        _1_DeliveryFormat.DataSource = Enum.GetValues<SmtpDeliveryFormat>();
    }

    void fSetting_Load(object sender, EventArgs e)
    {
        try
        {
            SettingFormat? _json = JsonSerializer.Deserialize<SettingFormat>(File.ReadAllText(AppLock.PathJson));

            if (_json == null)
                throw new ArgumentNullException(nameof(_json));

            _SplitSeparator.Text = _json.SplitSeparator;
            tabControl1.SelectedIndex = _json.Selected.GetValueOrDefault();
            SmtpFormat? _op1 = _json.Option_1;

            if (_op1 == null)
                return;

            _1_Timeout.Value = _op1.Timeout.GetValueOrDefault();
            _1_TargetName.Text = _op1.TargetName;
            _1_Port.Value = _op1.Port.GetValueOrDefault();
            _1_PickupDirectoryLocation.Text = _op1.PickupDirectoryLocation;
            _1_Host.Text = _op1.Host;
            _1_EnableSsl.Checked = _op1.EnableSsl.GetValueOrDefault();
            _1_DeliveryMethod.SelectedItem = _op1.DeliveryMethod;
            _1_DeliveryFormat.SelectedItem = _op1.DeliveryFormat;
            _1_UseDefaultCredentials.Checked = _op1.UseDefaultCredentials.GetValueOrDefault();
            _1_UserName.Text = _op1.UserName;
            _1_Password.Text = _op1.Password;
        }
        catch (Exception _ex)
        {
            MessageBox.Show(_ex.Message);
        }
    }

    async void button1_Click(object sender, EventArgs e)
    {
        SettingFormat _format = new()
        {
            SplitSeparator = _SplitSeparator.Text,
            Selected = tabControl1.SelectedIndex,

            Option_1 = new SmtpFormat
            {
                Timeout = (int?)_1_Timeout.Value,
                TargetName = _1_TargetName.Text,
                Port = (int?)_1_Port.Value,
                PickupDirectoryLocation = _1_PickupDirectoryLocation.Text,
                Host = _1_Host.Text,
                EnableSsl = _1_EnableSsl.Checked,
                DeliveryMethod = (SmtpDeliveryMethod?)_1_DeliveryMethod.SelectedItem,
                DeliveryFormat = (SmtpDeliveryFormat?)_1_DeliveryFormat.SelectedItem,
                UseDefaultCredentials = _1_UseDefaultCredentials.Checked,
                UserName = _1_UserName.Text,
                Password = _1_Password.Text
            }
        };

        string _json = JsonSerializer.Serialize(_format);
        await File.WriteAllTextAsync(AppLock.PathJson, _json);

        MessageBox.Show("Guardar...");
        Close();
    }
}
