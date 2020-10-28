using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Pixiv.OAuth;
using WindowsFormsApp1.Pixiv.Utils;

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
            UsernameBox.KeyPress += new KeyPressEventHandler(CheckEnter);
            PasswordBox.KeyPress += new KeyPressEventHandler(CheckEnter);
            checkLogin();
        }

        private async void loginButton_Click(object sender, EventArgs e)
        {
            if (UsernameBox.Text != "" && PasswordBox.Text != "")
            {
                MainForm mainForm = (MainForm)Owner;
                try
                {
                    mainForm.LoginTokens = await Auth.AuthorizeAsync(UsernameBox.Text, PasswordBox.Text);
                    checkLogin();
                }
                catch
                {
                    MessageBox.Show("使用者名稱或密碼錯誤", "登入失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("請輸入 Pixiv Id/Email", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CheckEnter(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                loginButton.Focus();
                loginButton_Click(sender, e);
                PasswordBox.Focus();
            }
        }

        private void checkLogin()
        {
            if (Properties.Settings.Default.KEY_PIXIV_USER_NAME != null)
            {

                accoutLabel.Text = Properties.Settings.Default.KEY_PIXIV_USER_NAME;
                usernameLabel.Text = Properties.Settings.Default.KEY_PIXIV_USER_USERNAME;
                UsernameBox.Text = "";
                PasswordBox.Text = "";
                profileImg.Load(Properties.Settings.Default.KEY_PIXIV_USER_IMG);
                accountControl.SelectedIndex = 1;
            }
            else
            {
                accoutLabel.Text = "名稱";
                usernameLabel.Text = "ID";
                accountControl.SelectedIndex = 0;
            }
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            SaveUserData.ClearAuthData();
            checkLogin();
        }
    }
}
