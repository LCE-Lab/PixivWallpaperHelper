using Microsoft.Win32;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;
using PixivWallpaperHelper.Pixiv.OAuth;
using System.IO;
using PixivWallpaperHelper.Utils;
using System.Diagnostics;

namespace PixivWallpaperHelper
{
    public partial class MainForm : Form
    {
        private string currentImagePath = "";
        private string _url = "";
        private SettingForm settingForm;
        private WallpaperFetcher wallpaper;
        public MainForm()
        {
            InitializeComponent();
            this.settingForm = new SettingForm();
            this.wallpaper = new WallpaperFetcher();
        }

        private static string appGuid = "7bcbe405-0325-4f8d-8527-afd151d13ff4";

        private void MainForm_Load(object sender, EventArgs e)
        {
            Mutex mutex = new Mutex(false, appGuid);
            if (!mutex.WaitOne(0, false))
            {
                MessageBox.Show("Instance already running");
                this.Close();
            }
            this.changeThumbnail();
            this.RegisterEvent();
            this.wallpaper.fetchWallpaper();
            backgroundWorker1.DoWork += new DoWorkEventHandler(FetchEvent);
            if (!backgroundWorker1.IsBusy) backgroundWorker1.RunWorkerAsync();
        }

        private void Form1_Click(object sender, EventArgs e) {
            this.menuStrip1.Visible = !this.menuStrip1.Visible;
            this.titlePanel.Visible = !this.titlePanel.Visible;
        }
        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            if (this.Width < 768) this.Width = 768;
            if (this.Height < 432) this.Height = 432;
        }

        private void wallpaperRefreshTimer_Tick(object sender, EventArgs e)
        {
            this.changeThumbnail();
        }

        private void 設定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settingForm.ShowDialog(this);
        }

        private void 重新整理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.changeThumbnail();
        }

        private void menuStrip1_Paint(object sender, PaintEventArgs e)
        {
            LinearGradientBrush brush = new LinearGradientBrush(
                this.menuStrip1.ClientRectangle,
                Color.FromArgb(204, 0, 0, 0),
                Color.FromArgb(0, 0, 0, 0),
                90F
            );
            e.Graphics.FillRectangle(brush, this.menuStrip1.ClientRectangle);
        }
        private void titlePanel_Paint(object sender, PaintEventArgs e)
        {
            LinearGradientBrush brush = new LinearGradientBrush(
                this.titlePanel.ClientRectangle,
                Color.FromArgb(0, 0, 0, 0),
                Color.FromArgb(204, 0, 0, 0),
                90F
            );
            e.Graphics.FillRectangle(brush, this.titlePanel.ClientRectangle);
        }

        private void changeThumbnail() {
            if (this.isWallpaperColorOnly()) {
                if (this.BackgroundImage != null) {
                    this.BackgroundImage.Dispose();
                    this.BackgroundImage = null;
                    this.currentImagePath = "";
                };
                this.BackColor = this.getWallpaperColor();
                _url = "";
                titleLabel.Text = "純色桌布";
                authorLabel.Text = "這似乎不是由本程式自動下載的相片輪播圖庫，請檢查桌布設定";
            }else
            {
                string newPath = this.getCurrentWallpaperPath();
                if (!newPath.Equals(this.currentImagePath))
                {
                    this.BackColor = Color.Black;
                    this.currentImagePath = newPath;
                    if (!File.Exists(newPath))
                    {
                        titleLabel.Text = "無法載入預覽";
                        authorLabel.Text = "桌布原始圖片似乎被刪除了";
                        _url = "";
                        return;
                    }
                    System.Drawing.Image image = System.Drawing.Image.FromFile(newPath);
                    if (this.BackgroundImage != null) this.BackgroundImage.Dispose();
                    this.BackgroundImage = image;
                    LocalArtwork localArtwork;
                    switch (wallpaper.getWallpaperInfo(newPath, out localArtwork))
                    {
                        case WallPaperInfoStatus.SUCCESS:
                            titleLabel.Text = localArtwork.Title;
                            authorLabel.Text = localArtwork.Author;
                            _url = localArtwork.WebUrl;
                            break;
                        case WallPaperInfoStatus.NOTFOUND:
                            titleLabel.Text = "未命名的桌布";
                            authorLabel.Text = "找不到此桌布的資訊";
                            _url = "";
                            break;
                        case WallPaperInfoStatus.PATHINVALID:
                            titleLabel.Text = "未命名的桌布";
                            authorLabel.Text = "這似乎不是由本程式自動下載的相片輪播圖庫，請檢查桌布設定";
                            _url = "";
                            break;
                    }
                }
            }
        }

        private Boolean isWallpaperColorOnly()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop");
            if (key != null)
            {
                string path = (string)key.GetValue("WallPaper");
                return path.Equals("");
            }
            else {
                return false;
            }
        }

        private Color getWallpaperColor() {
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
                return Color.FromArgb(0,0,0);
            }
        }

        private string getCurrentWallpaperPath()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop");
            if (key != null)
            {
                byte[] encodedPath = (byte[])key.GetValue("TranscodedImageCache");
                char[] chars = new char[(encodedPath.Length - 24) / sizeof(char)];
                System.Buffer.BlockCopy(encodedPath, 24, chars, 0, encodedPath.Length - 24);
                string str = new string(chars);
                return str.Split('\0')[0];
            } else
            {
                return "";
            }
        }

        private void RegisterEvent()
        {
            this.menuStrip1.Paint += new PaintEventHandler(this.menuStrip1_Paint);
            this.titlePanel.Paint += new PaintEventHandler(this.titlePanel_Paint);
            this.Click += new EventHandler(this.Form1_Click);
            this.ResizeEnd += new EventHandler(this.Form1_ResizeEnd);
        }

        private void titleLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_url != "") {
                Process.Start(_url);
            }
        private void FetchEvent(object sender, DoWorkEventArgs e)
        {
            Wallpaper.FetchWallpaper();
        }
    }
}
