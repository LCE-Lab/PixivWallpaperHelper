using Microsoft.Win32;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;
using WindowsFormsApp1.Pixiv.OAuth;

namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        private string currentImagePath = "";
        public MainForm()
        {
            InitializeComponent();
        }

        private Tokens tokens;
        public Tokens LoginTokens
        {
            set
            {
                tokens = value;
            }
        }
        private static string appGuid = "7bcbe405-0325-4f8d-8527-afd151d13ff4";
        private static string path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\PixivWallpapers";

        private void MainForm_Load(object sender, EventArgs e)
        {
            Mutex mutex = new Mutex(false, appGuid);
            if (!mutex.WaitOne(0, false))
            {
                MessageBox.Show("Instance already running");
                this.Close();
            }
            this.changeThumbnail();
            this.menuStrip1.Paint += new PaintEventHandler(this.menuStrip1_Paint);
            this.titlePanel.Paint += new PaintEventHandler(this.titlePanel_Paint);
            this.Click += new EventHandler(this.Form1_Click);
            this.ResizeEnd += new EventHandler(this.Form1_ResizeEnd);
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
            SettingForm settingForm = new SettingForm();
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
            }else
            {
                string newPath = this.getCurrentWallpaperPath();
                if (!newPath.Equals(this.currentImagePath))
                {
                    this.BackColor = Color.Black;
                    this.currentImagePath = newPath;
                    Image image = Image.FromFile(newPath);
                    if (this.BackgroundImage != null) this.BackgroundImage.Dispose();
                    this.BackgroundImage = image;
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
    }
}
