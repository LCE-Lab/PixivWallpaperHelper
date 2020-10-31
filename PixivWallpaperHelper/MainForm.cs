using Microsoft.Win32;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;
using PixivWallpaperHelper.Pixiv.Util;

namespace PixivWallpaperHelper
{
    public partial class MainForm : Form
    {
        private string _currentImagePath = "";
        private readonly SettingForm _settingForm;
        private readonly WallpaperFetcher _wallpaper;

        public MainForm()
        {
            InitializeComponent();
            _settingForm = new SettingForm();
            _wallpaper = new WallpaperFetcher();
        }

        private const string appGuid = "7bcbe405-0325-4f8d-8527-afd151d13ff4";

        private void MainForm_Load(object sender, EventArgs e)
        {
            var mutex = new Mutex(false, appGuid);
            if (!mutex.WaitOne(0, false))
            {
                MessageBox.Show("Instance already running");
                Close();
            }

            ChangeThumbnail();
            RegisterEvent();
            WallpaperFetcher.FetchWallpaper();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            menuStrip1.Visible = !menuStrip1.Visible;
            titlePanel.Visible = !titlePanel.Visible;
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            if (Width < 768) Width = 768;
            if (Height < 432) Height = 432;
        }

        private void wallpaperRefreshTimer_Tick(object sender, EventArgs e)
        {
            ChangeThumbnail();
        }

        private void 設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _settingForm.ShowDialog(this);
        }

        private void 重新整理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeThumbnail();
        }

        private void menuStrip1_Paint(object sender, PaintEventArgs e)
        {
            var brush = new LinearGradientBrush(
                menuStrip1.ClientRectangle,
                Color.FromArgb(204, 0, 0, 0),
                Color.FromArgb(0, 0, 0, 0),
                90F
            );
            e.Graphics.FillRectangle(brush, menuStrip1.ClientRectangle);
        }

        private void titlePanel_Paint(object sender, PaintEventArgs e)
        {
            var brush = new LinearGradientBrush(
                titlePanel.ClientRectangle,
                Color.FromArgb(0, 0, 0, 0),
                Color.FromArgb(204, 0, 0, 0),
                90F
            );
            e.Graphics.FillRectangle(brush, titlePanel.ClientRectangle);
        }

        private void ChangeThumbnail()
        {
            if (isWallpaperColorOnly())
            {
                if (BackgroundImage != null)
                {
                    BackgroundImage.Dispose();
                    BackgroundImage = null;
                    _currentImagePath = "";
                }

                BackColor = GetWallpaperColor();
            }
            else
            {
                var newPath = GetCurrentWallpaperPath();
                if (newPath.Equals(_currentImagePath)) return;
                BackColor = Color.Black;
                _currentImagePath = newPath;
                var image = Image.FromFile(newPath);
                BackgroundImage?.Dispose();
                BackgroundImage = image;
            }
        }

        private Boolean isWallpaperColorOnly()
        {
            var key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop");
            if (key != null)
            {
                var path = (string) key.GetValue("WallPaper");
                return path.Equals("");
            }

            return false;
        }

        private Color GetWallpaperColor()
        {
            var key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Colors");
            if (key != null)
            {
                var colorRaw = (string) key.GetValue("Background");
                var color = colorRaw.Split(' ');
                return Color.FromArgb(
                    Convert.ToInt32(color[0]),
                    Convert.ToInt32(color[1]),
                    Convert.ToInt32(color[2])
                );
            }

            return Color.FromArgb(0, 0, 0);
        }

        private string GetCurrentWallpaperPath()
        {
            var key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop");
            if (key != null)
            {
                var encodedPath = (byte[]) key.GetValue("TranscodedImageCache");
                var chars = new char[(encodedPath.Length - 24) / sizeof(char)];
                Buffer.BlockCopy(encodedPath, 24, chars, 0, encodedPath.Length - 24);
                var str = new string(chars);
                return str.Split('\0')[0];
            }

            return "";
        }

        private void RegisterEvent()
        {
            menuStrip1.Paint += menuStrip1_Paint;
            titlePanel.Paint += titlePanel_Paint;
            Click += Form1_Click;
            ResizeEnd += Form1_ResizeEnd;
        }
    }
}
