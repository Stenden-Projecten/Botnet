using System;

namespace Ananke.Modules {
    public class SysInfoModule : IControlModule {
        public string Identifier => "SYSINFO";

        public string Activate() {
            return Identifier;
        }

        public void ResponseReceived(string response) {
            throw new NotImplementedException();
        }
    }
}
