using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PixivWallpaperHelper.Pixiv.OAuth;
using PixivWallpaperHelper.Pixiv.Utils;

namespace PixivWallpaperHelper
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

        private void ValueChangedEvent(Object sender, EventArgs e)
        {
            var settings = new Dictionary<string, dynamic>
            {
                { "countNum", countNum.Value } ,
                { "originPictureCheck", originalPictureCheck.Checked } ,
                { "deleteCheck", deleteCheck.Checked } ,
                { "modeCombo", modeCombo.SelectedItem.ToString() } ,
                { "rankModeCombo", rankModeCombo.SelectedItem.ToString() } ,
                { "privateColletcion", privateCollection.Checked } ,
                { "R18Check", R18Check.Checked } ,
                { "paintingCheck", paintingCheck.Checked } ,
                { "resolutionNum", resolutionNum.Value } ,
                { "viewCountNum", viewCountNum.Value } ,
                { "collectionNum", collectionNum.Value }
            };

            Data.SaveSettingsData(settings);
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            SetSetting();
            RegisterEvent();
            CheckLogin();
        }

        private async void loginButton_Click(object sender, EventArgs e)
        {
            if (UsernameBox.Text != "" && PasswordBox.Text != "")
            {
                MainForm mainForm = (MainForm)Owner;
                try
                {
                    mainForm.LoginTokens = await Auth.AuthorizeAsync(UsernameBox.Text, PasswordBox.Text);
                    CheckLogin();
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

        private void CheckLogin()
        {
            if (Properties.Auth.Default.KEY_PIXIV_USER_LOGIN)
            {

                accoutLabel.Text = Properties.Auth.Default.KEY_PIXIV_USER_NAME;
                usernameLabel.Text = Properties.Auth.Default.KEY_PIXIV_USER_USERNAME;
                UsernameBox.Text = "";
                PasswordBox.Text = "";
                profileImg.Load(Properties.Auth.Default.KEY_PIXIV_USER_IMG);
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
            Data.ClearAuthData();
            CheckLogin();
        }

        private void RegisterEvent()
        {
            // 登入 Enter Event
            UsernameBox.KeyPress += new KeyPressEventHandler(CheckEnter);
            PasswordBox.KeyPress += new KeyPressEventHandler(CheckEnter);

            // 選項 Event
            countNum.ValueChanged += new EventHandler(ValueChangedEvent);
            originalPictureCheck.CheckedChanged += new EventHandler(ValueChangedEvent);
            deleteCheck.CheckedChanged += new EventHandler(ValueChangedEvent);
            modeCombo.SelectedValueChanged += new EventHandler(ValueChangedEvent);
            rankModeCombo.SelectedValueChanged += new EventHandler(ValueChangedEvent);
            privateCollection.CheckedChanged += new EventHandler(ValueChangedEvent);
            R18Check.CheckedChanged += new EventHandler(ValueChangedEvent);
            paintingCheck.CheckedChanged += new EventHandler(ValueChangedEvent);
            resolutionNum.ValueChanged += new EventHandler(ValueChangedEvent);
            viewCountNum.ValueChanged += new EventHandler(ValueChangedEvent);
            collectionNum.ValueChanged += new EventHandler(ValueChangedEvent);
        }

        private void SetSetting()
        {
            var settingsData = Data.GetSettingsData();

            foreach (var results in settingsData)
            {
                switch (results.Key)
                {
                    case "countNum":
                        countNum.Value = results.Value;
                        break;
                    case "originPictureCheck":
                        originalPictureCheck.Checked = results.Value;
                        break;
                    case "deleteCheck":
                        deleteCheck.Checked = results.Value;
                        break;
                    case "modeCombo":
                        modeCombo.SelectedItem = results.Value;
                        break;
                    case "rankModeCombo":
                        rankModeCombo.SelectedItem = results.Value;
                        break;
                    case "privateColletcion":
                        privateCollection.Checked = results.Value;
                        break;
                    case "R18Check":
                        R18Check.Checked = results.Value;
                        break;
                    case "paintingCheck":
                        paintingCheck.Checked = results.Value;
                        break;
                    case "resolutionNum":
                        resolutionNum.Value = results.Value;
                        break;
                    case "viewCountNum":
                        viewCountNum.Value = results.Value;
                        break;
                    case "collectionNum":
                        collectionNum.Value = results.Value;
                        break;
                }
            }
        }
    }
}
