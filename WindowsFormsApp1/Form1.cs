using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void fastTimer_Tick(object sender, EventArgs e)
        {
            this.currentWallpaper.Height = this.Height;
            this.currentWallpaper.Width = this.Width;
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            this.currentWallpaper.Image.Dispose();
            this.currentWallpaper.Image = Image.FromFile(this.getCurrentWallpaperPath());
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
            this.currentWallpaper.Image.Dispose();
            this.currentWallpaper.Image = Image.FromFile(this.getCurrentWallpaperPath());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.currentWallpaper.Image = Image.FromFile(this.getCurrentWallpaperPath());
        }
    }
}
