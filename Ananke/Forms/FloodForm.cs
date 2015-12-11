using System.Net;
using System.Windows.Forms;

namespace Ananke.Forms {
    public partial class FloodForm : Form {
        public IPAddress Target => txtTarget.IPAddress;
        public int Port => (int)numPort.Value;

        public FloodForm() {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, System.EventArgs e) {
            if(txtTarget.IsBlank || txtTarget.IPAddress == null) {
                MessageBox.Show(@"Must specify a valid target IP", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult = DialogResult.OK;
        }

        private void btnStop_Click(object sender, System.EventArgs e) {
            DialogResult = DialogResult.Abort;
        }

        private void btnCancel_Click(object sender, System.EventArgs e) {
            DialogResult = DialogResult.Cancel;
        }
    }
}
