using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CopyingCutingClipboard.Controllers
{
    public class CommandController
    {
        private static uint KEYEVENTF_KEYUP = 2;
        private static byte VK_CONTROL = 0x11;
        public static byte C_KEY = 0x43;
        public static byte V_KEY = 0x56;
        public static byte Z_KEY = 0x5A;
        public static void SendCtrlWithKey(byte key)
        {
            ThreadController.SetForegroundWindow();
            //SetForegroundWindow(hWnd);
            keybd_event(VK_CONTROL, 0, 0, 0);
            keybd_event(key, 0, 0, 0); //Send the C key (43 is "C")
            keybd_event(key, 0, KEYEVENTF_KEYUP, 0);
            keybd_event(VK_CONTROL, 0, KEYEVENTF_KEYUP, 0);// 'Left Control Up
        }

        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
    }
}
