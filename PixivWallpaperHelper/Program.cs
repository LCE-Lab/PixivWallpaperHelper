using System;
using System.Threading;
using System.Windows.Forms;

namespace PixivWallpaperHelper
{
    static class Program
    {
        private static readonly string AppGuid = "7bcbe405-0325-4f8d-8527-afd151d13ff4";
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Mutex mutex = new Mutex(false, AppGuid, out bool flag);
            mutex.WaitOne();
            if (flag)
            {
                Application.Run(new MainForm());
            }
            mutex.ReleaseMutex();
        }
    }
}
