using System;
using System.Windows.Forms;

namespace Ananke {
    public partial class ConnectForm : Form {
        public string Host => txtHost.Text;

        public ConnectForm() {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
        }

        private void txtHost_KeyPress(object sender, KeyPressEventArgs e) {
            // Close the dialog when enter is pressed
            if(e.KeyChar == (int)Keys.Enter) {
                DialogResult = DialogResult.OK;
                e.Handled = true;
            }
        }
    }
}
