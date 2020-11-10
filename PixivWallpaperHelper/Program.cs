using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace PixivWallpaperHelper
{
    static class WinAPI
    {
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint RegisterWindowMessage(string lpString);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        public static extern IntPtr SetForegroundWindow(IntPtr hWnd);

        public const uint HWND_BROADCAST = 0xFFFF;
        public const short SW_RESTORE = 9;
    }
    static class Program
    {
        public static uint BringToFrontMessage;
        private static readonly string AppGuid = "7bcbe405-0325-4f8d-8527-afd151d13ff4";
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            BringToFrontMessage = WinAPI.RegisterWindowMessage("PixivWallpaperHelperBringToFront");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Mutex mutex = new Mutex(false, AppGuid);
            try
            {

                if (mutex.WaitOne(0, false))
                {
                    Application.Run(new MainForm());
                }
                else
                {
                    WinAPI.PostMessage(
                      (IntPtr)WinAPI.HWND_BROADCAST,
                      BringToFrontMessage,
                      IntPtr.Zero,
                      IntPtr.Zero);
                    Environment.Exit(1);
                }
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }
    }
}
