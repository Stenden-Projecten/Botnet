using System;
using System.Windows.Forms;

namespace Ananke {
    public partial class ConnectForm : Form {
        public string Host {
            get { return txtHost.Text; }
        }

        public ConnectForm() {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
        }

        private void txtHost_KeyPress(object sender, KeyPressEventArgs e) {
            if(e.KeyChar == (int)Keys.Enter) {
                DialogResult = DialogResult.OK;
                e.Handled = true;
            }
        }
    }
}
