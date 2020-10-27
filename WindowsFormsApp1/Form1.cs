using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private string currentImagePath = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            this.changeThumbnail();
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

        private void wallpaperRefreshTimer_Tick(object sender, EventArgs e)
        {
            this.changeThumbnail();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.changeThumbnail();
            this.menuStrip1.Paint += new PaintEventHandler(this.menuStrip1_Paint);
            this.titlePanel.Paint += new PaintEventHandler(this.titlePanel_Paint);
        }

        private void settingButton_Click(object sender, EventArgs e)
        {
            SettingForm settingForm = new SettingForm();
            settingForm.ShowDialog(this);
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
            using (LinearGradientBrush brush = new LinearGradientBrush(this.menuStrip1.ClientRectangle,
                                                               Color.Black,
                                                               Color.FromArgb(0,0,0,0),
                                                               90F))
            {
                e.Graphics.FillRectangle(brush, this.menuStrip1.ClientRectangle);
            }
        }
        private void titlePanel_Paint(object sender, PaintEventArgs e)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(this.titlePanel.ClientRectangle,
                                                               Color.FromArgb(0, 0, 0, 0),
                                                               Color.Black,
                                                               90F))
            {
                e.Graphics.FillRectangle(brush, this.titlePanel.ClientRectangle);
            }
        }
    }
}
