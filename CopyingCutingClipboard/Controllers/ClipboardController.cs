using CopyingCutingClipboard.Common;
using CopyingCutingClipboard.NativeMethod;
using CopyingCutingClipboard.NativeMethod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CopyingCutingClipboard.Controllers
{
    public class ClipboardController
    {

        public IntPtr NextClipboardViewer;

        private List<CopiedText> copiedTextList;
        public ClipboardController()
        {
            copiedTextList = FileController.ReadObjectFromFile<List<CopiedText>>();
            if (copiedTextList == null)
            {
                copiedTextList = new List<CopiedText>();

            }
        }

        public void Add(CopiedText text)
        {
            copiedTextList.Add(text);
        }

        public void Remove(CopiedText text)
        {
            copiedTextList.Remove(text);
        }

        public bool Contains(CopiedText text)
        {
            return copiedTextList.Contains(text);
        }

        public void Add(string copiedText, string format)
        {

            if (!copiedTextList.Any(en => en.Text == copiedText))
            {
                if (!string.IsNullOrEmpty(copiedText))
                {
                    CopiedText newCopy = new CopiedText()
                    {
                        Text = copiedText,
                        Order = Count + 1,
                        Date = DateTime.Now,
                        DataFormat = format
                    };
                    copiedTextList.Add(newCopy);
                    //FileController.WriteStringToFile(@"D://Keylogger1.Txt", copiedText);
                }
            }
        }

        public void Add(string copiedText, string richText, string format)
        {

            if (!copiedTextList.Any(en => en.Text == copiedText))
            {
                if (!string.IsNullOrEmpty(copiedText))
                {
                    CopiedText newCopy = new CopiedText()
                    {
                        Text = copiedText,
                        Order = Count + 1,
                        RichText = richText,
                        Date = DateTime.Now,
                        DataFormat = format
                    };
                    copiedTextList.Add(newCopy);
                    FileController.WriteObjectToFile<List<CopiedText>>(copiedTextList);
                }
            }
        }

        public void SetClipboard(int index)
        {
            if (Count > 0)
            {
                if (!string.IsNullOrEmpty(copiedTextList[index].Text))
                {
                    SetClipboardText(copiedTextList[index].Text);
                }
            }
        }
        public CopiedText this[int index]
        {
            get
            {
                if (Count < index || index < 0)
                    return null;
                return copiedTextList[index];
            }
            set
            {
                copiedTextList[index] = value;
            }
        }
        public IEnumerator<CopiedText> GetEnumerator()
        {
            foreach (var text in copiedTextList)
                yield return text;
        }

        public int Count { get { return copiedTextList.Count; } }

        public static string GetClipboardText()
        {
            IntPtr hInstance = IntPtr.Zero;
            ClipboardNativeMethods.OpenClipboard(hInstance);
            IntPtr buffer = ClipboardNativeMethods.GetClipboardData(13);
            string text = Marshal.PtrToStringUni(buffer);
            ClipboardNativeMethods.CloseClipboard();
            return text;
        }

        public static bool SetClipboardText(string text)
        {
            if (!ClipboardNativeMethods.OpenClipboard(IntPtr.Zero))
                return false;
            ClipboardNativeMethods.EmptyClipboard();
            IntPtr alloc = MemoryNativeMethods.GlobalAlloc(MemoryNativeMethods.GMEM_MOVEABLE | MemoryNativeMethods.GMEM_DDESHARE, (UIntPtr)(sizeof(char) * (text.Length + 1)));
            IntPtr gLock = MemoryNativeMethods.GlobalLock(alloc);

            if ((int)text.Length > 0)
                Marshal.Copy(text.ToCharArray(), 0, gLock, text.Length);
            MemoryNativeMethods.GlobalUnlock(alloc);
            ClipboardNativeMethods.SetClipboardData(13, alloc);
            ClipboardNativeMethods.CloseClipboard();
            return true;
        }
        public ClipboardActions GetClipboardAction(ref Message m, ref string text, ref string format)
        {
            const int WM_PASTE = 0x0302;
            // defined in winuser.h
            const int WM_DRAWCLIPBOARD = 0x308;
            const int WM_CHANGECBCHAIN = 0x030D;

            switch (m.Msg)
            {
                case WM_DRAWCLIPBOARD:
                    DisplayClipboardData(ref text, ref format);
                    ClipboardNativeMethods.SendMessage(NextClipboardViewer, m.Msg, m.WParam, m.LParam);
                    return ClipboardActions.DrawClipboard;

                case WM_CHANGECBCHAIN:
                    if (m.WParam == NextClipboardViewer)
                        NextClipboardViewer = m.LParam;
                    else
                        ClipboardNativeMethods.SendMessage(NextClipboardViewer, m.Msg, m.WParam, m.LParam);
                    return ClipboardActions.ChangeChain;
                case WM_PASTE:

                    return ClipboardActions.Paste;

                default:
                    return ClipboardActions.None;
            }
        }


        void DisplayClipboardData(ref string text, ref string format)
        {
            try
            {
                IDataObject iData = new DataObject();
                iData = Clipboard.GetDataObject();

                if (iData.GetDataPresent(DataFormats.Rtf))
                {
                    string rtfCopiedText = (string)iData.GetData(DataFormats.Rtf);
                    string copiedText = (string)iData.GetData(DataFormats.Text);
                    if (!string.IsNullOrEmpty(copiedText))
                    {
                        this.Add(copiedText, rtfCopiedText, DataFormats.Rtf);
                    }
                    text = rtfCopiedText;
                    format = DataFormats.Rtf;
                }
                else if (iData.GetDataPresent(DataFormats.Text))
                {
                    string copiedText = (string)iData.GetData(DataFormats.Text);
                    if (!string.IsNullOrEmpty(copiedText))
                    {
                        this.Add(copiedText, DataFormats.Text);
                    }
                    text = copiedText;
                    format = DataFormats.Text;
                }
                else
                {
                    text = "[Clipboard data is not RTF or ASCII Text]";
                    }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }



        // memory

    }
}
