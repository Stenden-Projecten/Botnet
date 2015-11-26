using System;
using System.Management;

namespace Spindle {
    class Program {
        static void Main(string[] args) {
            Console.Write("enter ip: ");
            string ip = Console.ReadLine();
            if(String.IsNullOrEmpty(ip)) ip = "127.0.0.1";

            //Console.WriteLine(GetObject("Win32_Processor", "ProcessorID"));
            Bot bot = new Bot(ip, GetObject("Win32_Processor", "ProcessorID"));

            Console.ReadKey();
        }

        public static string GetObject(string table, string property) {
            ManagementObjectSearcher search = new ManagementObjectSearcher("SELECT " + property + " FROM " + table);
            var enu = search.Get().GetEnumerator();
            if(enu.MoveNext()) {
                return (string)enu.Current[property];
            }

            return null;
        }
    }
}
