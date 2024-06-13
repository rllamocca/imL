using System.Windows.Forms;

namespace EmailDelivery
{
    public partial class fIndex : Form
    {
        public fIndex()
        {
            InitializeComponent();
        }

        private void List11_ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            using fSetting _using = new();
            _using.ShowDialog();
        }
    }
}
