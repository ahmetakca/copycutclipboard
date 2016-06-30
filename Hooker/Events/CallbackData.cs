using System;

namespace Hooker.Events
{
    internal struct CallbackData
    {
        private readonly IntPtr m_LParam;
        private readonly IntPtr m_WParam;

        public CallbackData(IntPtr wParam, IntPtr lParam)
        {
            m_WParam = wParam;
            m_LParam = lParam;
        }

        public IntPtr WParam
        {
            get { return m_WParam; }
        }

        public IntPtr LParam
        {
            get { return m_LParam; }
        }
    }
}
