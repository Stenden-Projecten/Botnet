using System;
using System.Windows.Forms;

namespace Ananke.Forms {
    public partial class DownloadForm : Form {
        public string File => txtUrl.Text?.Trim();
        public string Target => txtTarget.Text?.Trim();

        public DownloadForm() {
            InitializeComponent();
        }

        private void btnDownload_Click(object sender, EventArgs e) {
            if(String.IsNullOrEmpty(File)) {
                MessageBox.Show(@"Must specify a file to download", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
        }
    }
}
