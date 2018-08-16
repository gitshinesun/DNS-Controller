using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Threading;
using System.Diagnostics;

namespace ProxyMonitor
{
    class Program
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        public static int dalayTime = 2000;

        public static Thread _thrMainThread = null;

        public static void program_run()
        {
            ProcessStartInfo info = new ProcessStartInfo(@"ProxyService.exe");
            string wrapped = string.Format(@"install");
            info.Arguments = wrapped;
            info.UseShellExecute = true;
            info.Verb = "runas";
            info.WindowStyle = ProcessWindowStyle.Hidden;
            try
            {
                Process.Start(info);
                Thread.Sleep(200);
            }
            catch { }
        }

        public static void CallBack()
        {
            while (true)
            {
                program_run();

                Thread.Sleep(dalayTime);
            }
        }

        [MTAThread]
        static void Main(string[] args)
        {
            bool bNew;
            Mutex mutex = new Mutex(true, "ProxyMonitor", out bNew);
            if (bNew)
            {
                try
                {
                    var handle = GetConsoleWindow();

                    // Hide
                    ShowWindow(handle, SW_HIDE);

                    if ((_thrMainThread == null) || ((_thrMainThread != null) && (_thrMainThread.IsAlive == false)))
                    {
                        _thrMainThread = new Thread(delegate ()
                        {
                            CallBack();
                        });
                        _thrMainThread.Start();
                    }
                }
                catch { }

                mutex.ReleaseMutex();
            }
        }
    }
}
