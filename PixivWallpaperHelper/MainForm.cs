using Microsoft.Win32;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using PixivWallpaperHelper.Pixiv.OAuth;
using System.IO;
using PixivWallpaperHelper.Utils;
using System.Diagnostics;
using System.ComponentModel;
using System.Net;

namespace PixivWallpaperHelper
{
    public partial class MainForm : Form
    {
        private string CurrentImagePath = "";
        private string Url = "";
        private readonly SettingForm SettingForm;
        private readonly WallpaperFetcher WallpaperFetcher;
        private bool Error = false;

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (int)Program.BringToFrontMessage)
            {
                WinAPI.ShowWindow(Handle, WinAPI.SW_RESTORE);
                WinAPI.SetForegroundWindow(Handle);
            }

            base.WndProc(ref m);
        }

        public MainForm()
        {
            InitializeComponent();
            CreateHandle();
            RegisterEvent();
            SetupNotifyIcon();
            SettingForm = new SettingForm();
            WallpaperFetcher = new WallpaperFetcher();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Show();
            ChangeThumbnail();
            CheckWebView2();
        }

        private void Form1_Click(object sender, EventArgs e) {
            menuStrip1.Visible = !menuStrip1.Visible;
            titlePanel.Visible = !titlePanel.Visible;
        }
        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            if (Width < 768) { Width = 768; }
            if (Height < 432) { Height = 432; }
        }

        private void WallpaperRefreshTimer_Tick(object sender, EventArgs e)
        {
            ChangeThumbnail();
        }

        private void SettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ = SettingForm.ShowDialog(this);
        }

        private void RefreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeThumbnail();
        }

        private void MenuStrip1_Paint(object sender, PaintEventArgs e)
        {
            LinearGradientBrush brush = new LinearGradientBrush(
                menuStrip1.ClientRectangle,
                Color.FromArgb(204, 0, 0, 0),
                Color.FromArgb(0, 0, 0, 0),
                90F
            );
            e.Graphics.FillRectangle(brush, menuStrip1.ClientRectangle);
        }
        private void TitlePanel_Paint(object sender, PaintEventArgs e)
        {
            LinearGradientBrush brush = new LinearGradientBrush(
                titlePanel.ClientRectangle,
                Color.FromArgb(0, 0, 0, 0),
                Color.FromArgb(204, 0, 0, 0),
                90F
            );
            e.Graphics.FillRectangle(brush, titlePanel.ClientRectangle);
        }

        private void ChangeThumbnail()
        {
            if (IsWallpaperColorOnly())
            {
                if (BackgroundImage != null)
                {
                    BackgroundImage.Dispose();
                    BackgroundImage = null;
                    CurrentImagePath = "";
                };
                BackColor = GetWallpaperColor();
                Url = "";
                titleLabel.Text = "純色桌布";
                authorLabel.Text = "這似乎不是由本程式自動下載的相片輪播圖庫，請檢查桌布設定";
                if (WallpaperFetcher.IsLocalUnchangedWallpaperEmpty() && !backgroundWorker1.IsBusy) { backgroundWorker1.RunWorkerAsync(); };
            }
            else
            {
                string newPath = GetCurrentWallpaperPath();
                if (!newPath.Equals(CurrentImagePath))
                {
                    BackColor = Color.Black;
                    CurrentImagePath = newPath;
                    if (!File.Exists(newPath))
                    {
                        titleLabel.Text = "無法載入預覽";
                        authorLabel.Text = "桌布原始圖片似乎被刪除了";
                        Url = "";
                        WallpaperFetcher.ClearCurrentWallpaperMark();
                        if (WallpaperFetcher.IsLocalUnchangedWallpaperEmpty() && !backgroundWorker1.IsBusy) { backgroundWorker1.RunWorkerAsync(); };
                        return;
                    }
                    if (BackgroundImage != null) { BackgroundImage.Dispose(); }
                    BackgroundImage = System.Drawing.Image.FromFile(newPath);
                    switch (WallpaperFetcher.GetWallpaperInfoFromWallpaper(newPath, out LocalArtwork localArtwork))
                    {
                        case WallPaperInfoStatus.Success:
                            titleLabel.Text = localArtwork.Title;
                            authorLabel.Text = localArtwork.Author;
                            Url = localArtwork.WebUrl;
                            break;
                        case WallPaperInfoStatus.NotFound:
                            titleLabel.Text = "未命名的桌布";
                            authorLabel.Text = "找不到此桌布的資訊";
                            Url = "";
                            break;
                        case WallPaperInfoStatus.PathInvalid:
                            titleLabel.Text = "未命名的桌布";
                            authorLabel.Text = "這似乎不是由本程式自動下載的相片輪播圖庫，請檢查桌布設定";
                            Url = "";
                            break;
                    }
                }
                if (WallpaperFetcher.IsLocalUnchangedWallpaperEmpty() && !backgroundWorker1.IsBusy) { backgroundWorker1.RunWorkerAsync(); };
            }
        }

        private bool IsWallpaperColorOnly()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop");
            if (key != null)
            {
                string path = (string)key.GetValue("WallPaper");
                return path.Equals("");
            }
            else
            {
                return false;
            }
        }

        private Color GetWallpaperColor()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Colors");
            if (key != null)
            {
                string colorRaw = (string)key.GetValue("Background");
                string[] color = colorRaw.Split(' ');
                return Color.FromArgb(
                    Convert.ToInt32(color[0]),
                    Convert.ToInt32(color[1]),
                    Convert.ToInt32(color[2])
                );
            }
            else
            {
                return Color.FromArgb(0, 0, 0);
            }
        }

        private string GetCurrentWallpaperPath()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop");
            if (key != null)
            {
                byte[] encodedPath = (byte[])key.GetValue("TranscodedImageCache");
                char[] chars = new char[(encodedPath.Length - 24) / sizeof(char)];
                Buffer.BlockCopy(encodedPath, 24, chars, 0, encodedPath.Length - 24);
                string str = new string(chars);
                return str.Split('\0')[0];
            }
            else
            {
                return "";
            }
        }

        private void CheckWebView2()
        {
            RegistryKey key;
            if (Environment.Is64BitOperatingSystem)
            {
                key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Microsoft\EdgeUpdate\Clients\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}");
            }
            else
            {
                key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\EdgeUpdate\Clients\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}");
            }
            
            if (key == null)
            {
                DialogResult result = MessageBox.Show("登入功能需要使用 WebView 2 Runtime，是否關閉此程式並下載安裝 WebView 2 Runtime?", "缺少 WebView2", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {
                    Process.Start("https://developer.microsoft.com/zh-tw/microsoft-edge/webview2/consumer/");
                    Application.ExitThread();
                }
            }
        }

        private void RegisterEvent()
        {
            menuStrip1.Paint += new PaintEventHandler(MenuStrip1_Paint);
            titlePanel.Paint += new PaintEventHandler(TitlePanel_Paint);
            Click += new EventHandler(Form1_Click);
            ResizeEnd += new EventHandler(Form1_ResizeEnd);
            notifyIcon1.MouseClick += new MouseEventHandler(NotifyIcon1_MouseClick);
            FormClosing += new FormClosingEventHandler(MainForm_FormClosing);
        }

        private void TitleLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Url != "")
            {
                _ = Process.Start(Url);
            }
        }

        private void FetchEvent(object sender, DoWorkEventArgs e)
        {
            try
            {
                WallpaperFetcher.FetchWallpaper().Wait();
                Error = false;
            }
            catch (AggregateException e1)
            {
                if (!Error) { 
                    notifyIcon1.ShowBalloonTip(3000, Text, $"抓取圖片時發生網路錯誤: {e1.InnerException.Message}", ToolTipIcon.Error);
                    Error = true;
                }
                backgroundWorker1.CancelAsync();
            }
        }

        private void NotifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ShowForm();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (WindowState != FormWindowState.Minimized)
            {
                e.Cancel = true;
                WindowState = FormWindowState.Minimized;
                ShowInTaskbar = false;
            }
        }

        private void ShowForm(object sender = null, EventArgs e = null)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Show();
                WindowState = FormWindowState.Normal;
                ShowInTaskbar = true;
                Activate();
                Focus();
            }
        }

        private void SetupNotifyIcon()
        {
            ContextMenu menu = new ContextMenu();
            MenuItem menuOpen = new MenuItem("Open Pixiv Wallpaper Helper");
            MenuItem menuExit = new MenuItem("Quit Pixiv Wallpaper Helper");

            menuOpen.Click += new EventHandler(ShowForm);
            menuExit.Click += new EventHandler(Exit);

            menu.MenuItems.Add(menuOpen);
            menu.MenuItems.Add(menuExit);

            notifyIcon1.ContextMenu = menu;
        }

        private void Exit(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void ErrorNotifyCooldown_Tick(object sender, EventArgs e)
        {
            Error = false;
        }
    }
}
