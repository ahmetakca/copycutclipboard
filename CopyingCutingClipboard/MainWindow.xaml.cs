using CopyingCutingClipboard.Common;
using CopyingCutingClipboard.Controllers;
using Hooker;
using Hooker.Interfaces;
using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CopyingCutingClipboard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IKeyboardMouseEvents m_Events;

        public ClipboardController ClipboardController;
        bool LeftMouseIsPressed = false;
        private bool MouseOnForm = true;

        private int MaxTick = 5;
        private int TickCount = 0;
        private double OpacityLastValue = 0;
        private const int width = 100;
        private const int height = 100;
        private const double MinOpacityValue = 0.2;
        private const double MaxOpacityValue = 0.8;
        private const int TimerInterval = 50;
        private const int MinOpacityMaxTickValue = 15;

        private IntPtr hWndNextViewer;
        private HwndSource hWndSource;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            //ClipboardController = new ClipboardController();
            WindowInteropHelper wih = new WindowInteropHelper(this);
            HwndSource hWndSource = HwndSource.FromHwnd(wih.Handle);
            hWndSource.AddHook(this.WinProc);   // start processing window messages 
            hWndNextViewer = (IntPtr)NativeMethod.ClipboardNativeMethods.SetClipboardViewer((int)hWndSource.Handle);   // set this window as a viewer 
            //ClipboardController.NextClipboardViewer = (IntPtr)NativeMethod.ClipboardNativeMethods.SetClipboardViewer((int)this.Handle);

            //SubscribeGlobal();
            //timer = new Timer();
            //timer.Interval = TimerInterval;
            //timer.Tick += timer_Tick;
            //InitializePopupControls();
        }
        private IntPtr WinProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            //string text = string.Empty;
            //string format = string.Empty;
            //switch (ClipboardController.GetClipboardAction(ref m, ref text, ref format))
            //{
            //    case ClipboardActions.DrawClipboard:
            //        if (format == System.Windows.DataFormats.Rtf)
            //        {
            //            rtxtCopiedText.Rtf = text;
            //            ThreadController.SetLastFocusedProcess();
            //            ThreadController.SetActiveWindow();
            //        }
            //        else
            //        if (format == System.Windows.DataFormats.Text)
            //        {
            //            rtxtCopiedText.Text = text;
            //            ThreadController.SetLastFocusedProcess();
            //            ThreadController.SetActiveWindow();
            //        }
            //        break;
            //    case ClipboardActions.ChangeChain:
            //        break;
            //    case ClipboardActions.Paste:
            //        break;
            //    case ClipboardActions.None:
            //    default:
            //        base.WndProc(ref m);
            //        break;
            //}

            const int WM_PASTE = 0x0302;
            // defined in winuser.h
            const int WM_DRAWCLIPBOARD = 0x308;
            const int WM_CHANGECBCHAIN = 0x030D;
            switch (msg)
            {
                case WM_CHANGECBCHAIN:
                    if (wParam == hWndNextViewer)
                    {
                        // clipboard viewer chain changed, need to fix it. 
                        hWndNextViewer = lParam;
                    }
                    else if (hWndNextViewer != IntPtr.Zero)
                    {
                        // pass the message to the next viewer. 
                        NativeMethod.ClipboardNativeMethods.SendMessage(hWndNextViewer, msg, wParam, lParam);
                    }
                    break;

                case WM_DRAWCLIPBOARD:
                    // clipboard content changed 
                    this.DrawContent();
                    // pass the message to the next viewer. 
                    NativeMethod.ClipboardNativeMethods.SendMessage(hWndNextViewer, msg, wParam, lParam);
                    break;
            }

            return IntPtr.Zero;
        }
        private void DrawContent()
        {
            pnlContent.Children.Clear();

            if (Clipboard.ContainsText())
            {
                // we have some text in the clipboard. 
                TextBox tb = new TextBox();
                tb.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                tb.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                tb.Text = Clipboard.GetText();
                tb.IsReadOnly = true;
                tb.TextWrapping = TextWrapping.NoWrap;
                pnlContent.Children.Add(tb);
            }
            else if (Clipboard.ContainsFileDropList())
            {
                // we have a file drop list in the clipboard 
                ListBox lb = new ListBox();
                lb.ItemsSource = Clipboard.GetFileDropList();
                pnlContent.Children.Add(lb);
            }
            else if (Clipboard.ContainsImage())
            {
                // Because of a known issue in WPF, 
                // we have to use a workaround to get correct 
                // image that can be displayed. 
                // The image have to be saved to a stream and then 
                // read out to workaround the issue. 
                MemoryStream ms = new MemoryStream();
                BmpBitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(Clipboard.GetImage()));
                enc.Save(ms);
                ms.Seek(0, SeekOrigin.Begin);

                BmpBitmapDecoder dec = new BmpBitmapDecoder(ms,
                    BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);

                Image img = new Image();
                img.Stretch = Stretch.Uniform;
                img.Source = dec.Frames[0];
                pnlContent.Children.Add(img);
            }
            else
            {
                Label lb = new Label();
                lb.Content = "The type of the data in the clipboard is not supported by this sample.";
                pnlContent.Children.Add(lb);
            }
        }
        private void SubscribeApplication()
        {
            Unsubscribe();
            Subscribe(Hook.AppEvents());
        }

        private void SubscribeGlobal()
        {
            Unsubscribe();
            Subscribe(Hook.GlobalEvents());
        }

        private void Subscribe(IKeyboardMouseEvents events)
        {
            m_Events = events;
            //m_Events.MouseMove += HookManager_MouseMove;
            //m_Events.MouseDown += HookManager_MouseDown;
            //m_Events.MouseUp += HookManager_MouseUp;
            //m_Events.KeyDown += HookManager_KeyUp;
            //m_Events.KeyUp += HookManager_KeyDown;

        }

        private void Unsubscribe()
        {
            if (m_Events == null) return;
            //m_Events.MouseMove -= HookManager_MouseMove;
            //m_Events.MouseDown -= HookManager_MouseDown;
            //m_Events.MouseUp -= HookManager_MouseUp;
            //m_Events.KeyDown -= HookManager_KeyUp;
            //m_Events.KeyUp -= HookManager_KeyDown;
        }

        //private void HookManager_KeyUp(object sender, KeyEventArgs e)
        //{
        //    if (e.Control && e.Shift && e.KeyCode == Keys.V)
        //    {
        //        e.SuppressKeyPress = true;
        //        e.Handled = true;
        //    }
        //}

        //private void HookManager_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Control && e.Shift && e.KeyCode == Keys.V)
        //    {
        //        var p = MouseController.GetCursorPosition();
        //        ThreadController.SetLastFocusedProcess();
        //        this.ShowPopup(p);
        //        e.SuppressKeyPress = true;
        //        e.Handled = true;
        //    }
        //}


        //private void HookManager_MouseMove(object sender, MouseEventArgs e)
        //{
        //    if (this.Visible)
        //        ControlMouse(this.Bounds, MouseController.GetCursorPosition());
        //}

        //private void HookManager_MouseDown(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Left)
        //    {
        //        LeftMouseIsPressed = true;
        //    }
        //}

        //private void HookManager_MouseUp(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Left)
        //    {
        //        LeftMouseIsPressed = false;
        //    }
        //}

        //private bool ControlMouse(Rectangle r, Point p)
        //{
        //    if (p.X > r.X && p.Y > r.Y && p.X < (r.X + r.Width) && p.Y < (r.Y + r.Height))
        //    {
        //        if (!MouseOnForm)
        //        {
        //            /// mouse enter 
        //            MouseOnForm = true;
        //            StopTimer(true);
        //        }
        //    }
        //    else if (!LeftMouseIsPressed)
        //    {
        //        if (MouseOnForm)
        //        {
        //            /// mouse leave 
        //            MouseOnForm = false;
        //            StartTimer();
        //        }
        //    }
        //    return MouseOnForm;
        //}

    }
}
