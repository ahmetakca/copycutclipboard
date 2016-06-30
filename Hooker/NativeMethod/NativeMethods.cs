using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Hooker.NativeMethod
{
    // Because it is a P/Invoke method, 'GetSystemMetrics(int)'
    // should be defined in a class named NativeMethods, SafeNativeMethods,
    // or UnsafeNativeMethods.
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724385(v=vs.85).aspx
    internal static class NativeMethods
    {
        private const int SM_CXDRAG = 68;
        private const int SM_CYDRAG = 69;

        [DllImport("user32.dll")]
        private static extern int GetSystemMetrics(int index);

        public static int GetXDragThreshold()
        {
            return GetSystemMetrics(SM_CXDRAG);
        }

        public static int GetYDragThreshold()
        {
            return GetSystemMetrics(SM_CYDRAG);
        }
    }
}
