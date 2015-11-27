using System;

namespace Spindle {
    class Program {
        static void Main(string[] args) {
            Console.Write("enter ip: ");
            string ip = Console.ReadLine();
            if(String.IsNullOrEmpty(ip)) ip = "127.0.0.1";

            Console.WriteLine("ProcessorID = " + Util.GetObject("Win32_Processor", "ProcessorID"));
            Bot bot = new Bot(ip, Util.GetObject("Win32_Processor", "ProcessorID"));

            Console.ReadKey();
        }
    }
}
