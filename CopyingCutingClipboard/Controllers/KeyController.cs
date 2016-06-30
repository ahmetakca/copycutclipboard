using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CopyingCutingClipboard.Controllers
{
    public class KeyController
    {
        [DllImport("user32.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetAsyncKeyState(Keys basiLanTus);
        public static bool KeyPressed(Keys key)
        {
            return 0 == GetAsyncKeyState(key);
        }
        public static bool ControlCopy()
        {
            return KeyPressed(Keys.Control) && KeyPressed(Keys.C);
        }
    }
}
