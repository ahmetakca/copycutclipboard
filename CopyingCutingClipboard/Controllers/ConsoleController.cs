using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CopyingCutingClipboard.Controllers
{
    public class ConsoleController
    {
        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;

        public static void Show(IntPtr hwnd)
        {
            ShowWindow(hwnd, SW_SHOW);
        }

        public static void Hide(IntPtr hwnd)
        {
            ShowWindow(hwnd, SW_HIDE);
        }

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("Kernel32")]
        public static extern IntPtr GetConsoleWindow();

    }
}
