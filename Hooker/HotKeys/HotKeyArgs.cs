using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hooker.HotKeys
{
    public sealed class HotKeyArgs : EventArgs
    {
        private readonly DateTime m_TimeOfExecution;

        /// <summary>
        ///     Creates an instance of the HotKeyArgs.
        ///     <param name="triggeredAt">Time when the event was triggered</param>
        /// </summary>
        public HotKeyArgs(DateTime triggeredAt)
        {
            m_TimeOfExecution = triggeredAt;
        }

        /// <summary>
        ///     Time when the event was triggered
        /// </summary>
        public DateTime Time
        {
            get { return m_TimeOfExecution; }
        }
    }
}
