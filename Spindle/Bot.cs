using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using IRCLib;
using IRCLib.Data;

namespace Spindle {
    public class Bot {
        private readonly Client _ircClient;
        private readonly Dictionary<string, IBotModule> _modules = new Dictionary<string, IBotModule>(); 

        /// <summary>
        ///     
        /// </summary>
        /// <param name="host">IP of IRC command server</param>
        /// <param name="user">Username of the bot</param>
        public Bot(string host, string user) {
            LoadModules();

            _ircClient = new Client(host, new User(user));

            _ircClient.RawMessageSent += (sender, args) => Console.WriteLine(">> " + args.Message + "\n");
            _ircClient.RawMessageReceived += (sender, args) => Console.WriteLine("<< " + args.Message + "\n");

            _ircClient.SetHandler("001", OnServerReady);
            _ircClient.SetHandler("PRIVMSG", OnMessageReceived);

            if(!_ircClient.Connect()) {
                Console.WriteLine("Failed to connect");
            }
        }

        /// <summary>
        ///     Registers a new bot module
        /// </summary>
        /// <param name="module">Module to add</param>
        public void RegisterModule(IBotModule module) {
            _modules[module.Identifier] = module;
        }

        /// <summary>
        ///     Find and load all module assemblies in the Modules folder
        /// </summary>
        public void LoadModules() {
            string[] files = Directory.GetFiles("Modules", "*.dll"); // Get all .dll files
            Type moduleType = typeof(IBotModule); // Interface type to compare to

            foreach(string file in files) {
                Assembly assembly = Assembly.LoadFrom(file); // Load an assembly
                Type[] types = assembly.GetTypes();
                foreach(Type type in types) {
                    if (type.IsInterface || type.IsAbstract) continue; // Make sure it can be instantiated
                    if (type.GetInterface(moduleType.FullName) == null) continue; // Check if it implements IBotModule

                    RegisterModule((IBotModule)Activator.CreateInstance(type));
                }
            }
        }

        private void OnMessageReceived(Client client, Message message) {
            // check if message came from admin
            // TODO: verify this other way than username
            if(message.Source.User != "admin" && message.Source.User != "@admin") return;

            // strip IRC symbols
            string from = message.Source.User.TrimStart('@');

            // split into command and arguments
            string[] parts = message.Parameters[1].Split(' ');
            string command = parts[0];
            string args = null;
            if(parts.Length > 1) {
                for(int i = 1; i < parts.Length; i++) {
                    if(i > 1) {
                        args += " ";
                    }
                    args += parts[i];
                }
            }
            

            IBotModule module;
            if(_modules.TryGetValue(command, out module)) {
                string result = module.CommandReceived(args); // invoke the relevant module
                if(result != null) {
                    client.SendRaw("PRIVMSG {0} :{1}", from, result);
                }
            } else {
                Console.WriteLine("Unknown command " + command);
            }
        }

        private void OnServerReady(Client client, Message message) {
            client.SendRaw("JOIN #cnc");
        }
    }
}
