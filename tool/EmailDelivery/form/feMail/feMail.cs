using System;
using System.Windows.Forms;

namespace EmailDelivery
{
    public partial class feMail : Form
    {
        public feMail()
        {
            InitializeComponent();
        }
        public feMail(eMailCooked _cooked) : this()
        {
            //InitializeComponent();

            if (_cooked == null)
                return;

            _Code.Text = _cooked.Code;
            _Send.Checked = _cooked.Send.GetValueOrDefault();

            if (_cooked.To != null)
                foreach (string? _item in _cooked.To)
                    _To.Items.Add(_item);
            if (_cooked.CC != null)
                foreach (string? _item in _cooked.CC)
                    _CC.Items.Add(_item);
            if (_cooked.BCC != null)
                foreach (string? _item in _cooked.BCC)
                    _BCC.Items.Add(_item);

            _Subject.Text = _cooked.Subject;
            _Body.Text = _cooked.Body;

            if (_cooked.PathAttachments != null)
                foreach (string? _item in _cooked.PathAttachments)
                    _PathAttachments.Items.Add(_item);

            if (_cooked.Result == null)
            {
                _Result.Text = "Not processed";

                return;
            }

            if (_cooked.Result is bool _bool)
            {
                _Result.Text = _bool == true ? "Ok" : "¬Ok";

                return;
            }

            if (_cooked.Result is Exception _Exception)
            {
                _Result.Text = _Exception.Message;

                return;
            }
        }
    }
}
