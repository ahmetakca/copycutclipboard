using System.Collections.Generic;
using System.Windows.Forms;

namespace CopyingCutingClipboard.Common
{
    public class KeyStatus
    {
        private List<Keys> downKeys { get; set; } = new List<Keys>();

        public bool ControlPressed { get; set; } = false;

        public bool ShiftPressed { get; set; } = false;

        public bool AltPressed { get; set; } = false;

        public bool ControlCPressed { get; set; } = false;

        public bool ControlVPressed { get; set; } = false;

        public bool ControlShiftVPressed { get; set; } = false;


        public int Count { get { return downKeys.Count; } }

        public void Add(Keys key)
        {
            if (!Contains(key))
            {
                downKeys.Add(key);
                KeyOperations(key, true);
                if (Count == 2)
                {
                    if (key == Keys.C && ControlPressed)
                    {
                        ControlCPressed = true;
                    }
                    else if (key == Keys.X && ControlPressed)
                    {
                        ControlCPressed = true;
                    }
                    else if (key == Keys.V && ControlPressed)
                    {
                        ControlVPressed = true;
                    }
                }
                else if (Count == 3)
                {
                    if (key == Keys.V && ControlPressed && ShiftPressed)
                    {
                        ControlShiftVPressed = true;
                    }
                }
            }
        }

        public void Remove(Keys key)
        {
            downKeys.Remove(key);
            KeyOperations(key, false);
            ControlCPressed = false;
            ControlCPressed = false;
            ControlVPressed = false;
            ControlShiftVPressed = false;
        }

        private void KeyOperations(Keys key, bool isDown)
        {
            switch (key)
            {
                case Keys.LControlKey:
                case Keys.RControlKey:
                    ControlPressed = isDown;
                    break;
                case Keys.LShiftKey:
                case Keys.RShiftKey:
                    ShiftPressed = isDown;
                    break;
                case Keys.Alt:
                    AltPressed = isDown;
                    break;
            }
        }

        public bool Contains(Keys key)
        {
            return downKeys.Contains(key);
        }


        public Keys this[int index]
        {
            get
            {
                if (Count < index || index < 0)
                    return Keys.None;
                return downKeys[index];
            }
            set
            {
                downKeys[index] = value;
            }
        }
        public IEnumerator<Keys> GetEnumerator()
        {
            foreach (var key in downKeys)
                yield return key;
        }
    }
}
