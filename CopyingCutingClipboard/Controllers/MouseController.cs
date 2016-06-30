using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace CopyingCutingClipboard.Controllers
{
    public class MouseController
    {
        public static Point CursorLastPosition;
        public static Point GetCursorPosition()
        {

            try
            {
                Point p;
                if (GetCursorPos(out p))
                {
                    CursorLastPosition = p;
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return CursorLastPosition;
        }
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point pt);
    }
}
