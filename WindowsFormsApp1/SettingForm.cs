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
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            InitializeComponent();
        }

        private void modeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.modeCombo.SelectedItem) {
                case "排行榜":
                    this.grabModeControl.SelectedIndex = 0;
                    break;
                case "推薦":
                    this.grabModeControl.SelectedIndex = 2;
                    break;
                case "收藏":
                    this.grabModeControl.SelectedIndex = 1;
                    break;
                default:
                    this.grabModeControl.SelectedIndex = 2;
                    break;
            }
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            this.modeCombo.SelectedItem = "排行榜";
            this.rankModeCombo.SelectedItem = "每週";
        }
    }
}
