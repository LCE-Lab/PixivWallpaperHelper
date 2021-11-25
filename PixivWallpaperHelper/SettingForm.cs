using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Windows.Forms;
using PixivWallpaperHelper.Pixiv.OAuth;
using PixivWallpaperHelper.Utils;

namespace PixivWallpaperHelper
{
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            InitializeComponent();
        }

        private void ModeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (modeCombo.SelectedItem)
            {
                case "排行榜":
                    grabModeControl.SelectedIndex = 0;
                    break;
                case "推薦":
                    grabModeControl.SelectedIndex = 2;
                    break;
                case "收藏":
                    grabModeControl.SelectedIndex = 1;
                    break;
                default:
                    grabModeControl.SelectedIndex = 2;
                    break;
            }
        }

        private void ValueChangedEvent(object sender, EventArgs e)
        {
            Dictionary<string, dynamic> settings = new Dictionary<string, dynamic>
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

        private async void LoginButton_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();

            RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();
            byte[] randomBytes = new byte[32];
            random.GetNonZeroBytes(randomBytes);

            string code_verifier = Base64Encode(randomBytes);
            string code_challenge = Base64Encode(ComputeHashSha256(Encoding.UTF8.GetBytes(code_verifier)));
            string url = $"https://app-api.pixiv.net/web/v1/login?code_challenge={code_challenge}&code_challenge_method=S256&client=pixiv-android";
            loginForm.SetUrl(url);

            if (loginForm.ShowDialog(this) == DialogResult.OK)
            {
                _ = await Auth.Login(code_verifier, loginForm.Code);
                CheckLogin();
            }
        }

        public static string Base64Encode(byte[] bytes)
        {
            // It is recommended to use a URL-safe string as code_verifier.
            // See section 4 of RFC 7636 for more details.
            return Convert.ToBase64String(bytes)
                        .TrimEnd('=')
                        .Replace('+', '-')
                        .Replace('/', '_');
        }

        public static byte[] ComputeHashSha256(byte[] toBeHashed)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(toBeHashed);
            }
        }

        private void CheckLogin()
        {
            if (Properties.Auth.Default.KEY_PIXIV_USER_LOGIN)
            {
                accoutLabel.Text = Properties.Auth.Default.KEY_PIXIV_USER_NAME;
                usernameLabel.Text = Properties.Auth.Default.KEY_PIXIV_USER_USERNAME;
                profileImg.Image = Image.SaveImage(Properties.Auth.Default.KEY_PIXIV_USER_IMG);
                accountControl.SelectedIndex = 1;
                modeCombo.Enabled = true;
                modeLabel.Enabled = true;
                rankModeCombo.Enabled = true;
                rankModeLabel.Enabled = true;
            }
            else
            {
                accoutLabel.Text = "名稱";
                usernameLabel.Text = "ID";
                accountControl.SelectedIndex = 0;
                modeCombo.SelectedItem = "排行榜";
                modeCombo.Enabled = false;
                modeLabel.Enabled = false;
                rankModeCombo.SelectedItem = "每週";
                rankModeCombo.Enabled = false;
                rankModeLabel.Enabled = false;
            }
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            Data.ClearAuthData();
            CheckLogin();
        }

        private void RegisterEvent()
        {
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
            Dictionary<string, dynamic> settingsData = Data.GetSettingsData();

            foreach (KeyValuePair<string, dynamic> results in settingsData)
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
