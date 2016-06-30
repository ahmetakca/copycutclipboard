using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace CopyingCutingClipboard.Controllers
{
    public class ApplicationController
    {
        //static ClipboardEventForm form;
        //static NotifyIcon notifyIcon;//notifyIcon
        //private static Mutex mutex = new Mutex(); // make sure that one instance is running at the time

        //static void onClick(object sender, EventArgs e)
        //{

        //    if (((MouseEventArgs)e).Button == MouseButtons.Right)
        //    {
        //        if (MessageBox.Show("Nereye Yavru Kurt?", "Copying Cuting Clipboard", MessageBoxButtons.YesNo) == DialogResult.Yes)
        //        {
        //            notifyIcon.Visible = false;
        //            Application.Exit();
        //        }
        //    }
        //    else if (((MouseEventArgs)e).Button == MouseButtons.Left)
        //    {
        //        form?.ShowPopup(MouseController.GetCursorPosition());
        //    }

        //}
        //public static void Start()
        //{
        //    mutex.WaitOne();

        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);
        //    ConsoleController.Hide(ConsoleController.GetConsoleWindow());
        //    form = new ClipboardEventForm();

        //    //notifyIcon = new NotifyIcon();
        //    //notifyIcon.Icon = new Icon(typeof(Program), "Icon.ico");
        //    //notifyIcon.Text = "Copying Cuting Clipboard";
        //    //notifyIcon.Click += new EventHandler(onClick);
        //    //notifyIcon.Visible = true;
        //    //ConsoleNativeMethods.SetConsoleCtrlHandler(new HandlerRoutine(ConsoleCtrlCheck), true);

        //    Application.Run();
        //    mutex.ReleaseMutex();

        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctrlType"></param>
        /// <returns></returns>
        //private static bool ConsoleCtrlCheck(CtrlTypes ctrlType)
        //{
        //    switch (ctrlType)
        //    {
        //        case CtrlTypes.CTRL_C_EVENT:
        //            break;

        //        case CtrlTypes.CTRL_BREAK_EVENT:
        //            Console.WriteLine("CTRL+BREAK received, shutting down");
        //            break;

        //        case CtrlTypes.CTRL_CLOSE_EVENT:
        //            Console.WriteLine("Program being closed, shutting down");
        //            break;

        //        case CtrlTypes.CTRL_LOGOFF_EVENT:
        //        case CtrlTypes.CTRL_SHUTDOWN_EVENT:
        //            Console.WriteLine("User is logging off!, shutting down");
        //            break;
        //    }

        //    return true;
        //}

    }
}
