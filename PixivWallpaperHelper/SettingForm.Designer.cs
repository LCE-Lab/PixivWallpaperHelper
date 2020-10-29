namespace PixivWallpaperHelper
{
    partial class SettingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.accountLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.UsernameBox = new System.Windows.Forms.TextBox();
            this.PasswordBox = new System.Windows.Forms.TextBox();
            this.loginButton = new System.Windows.Forms.Button();
            this.accountGroup = new System.Windows.Forms.GroupBox();
            this.accountControl = new System.Windows.Forms.TabControl();
            this.noLoginPage = new System.Windows.Forms.TabPage();
            this.loginPage = new System.Windows.Forms.TabPage();
            this.profileImg = new System.Windows.Forms.PictureBox();
            this.logoutButton = new System.Windows.Forms.Button();
            this.accoutLabel = new System.Windows.Forms.Label();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.grabOptionGroup = new System.Windows.Forms.GroupBox();
            this.grabModeControl = new System.Windows.Forms.TabControl();
            this.rankingPage = new System.Windows.Forms.TabPage();
            this.rankModeCombo = new System.Windows.Forms.ComboBox();
            this.rankModeLabel = new System.Windows.Forms.Label();
            this.collectionPage = new System.Windows.Forms.TabPage();
            this.privateCollection = new System.Windows.Forms.CheckBox();
            this.emptyPage = new System.Windows.Forms.TabPage();
            this.originalPictureCheck = new System.Windows.Forms.CheckBox();
            this.countNum = new System.Windows.Forms.NumericUpDown();
            this.countLabel = new System.Windows.Forms.Label();
            this.modeCombo = new System.Windows.Forms.ComboBox();
            this.modeLabel = new System.Windows.Forms.Label();
            this.deleteCheck = new System.Windows.Forms.CheckBox();
            this.filterOption = new System.Windows.Forms.GroupBox();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.collectionLabel = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.viewCountLabel = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.resolution = new System.Windows.Forms.Label();
            this.paintingCheck = new System.Windows.Forms.CheckBox();
            this.R18Check = new System.Windows.Forms.CheckBox();
            this.accountGroup.SuspendLayout();
            this.accountControl.SuspendLayout();
            this.noLoginPage.SuspendLayout();
            this.loginPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.profileImg)).BeginInit();
            this.grabOptionGroup.SuspendLayout();
            this.grabModeControl.SuspendLayout();
            this.rankingPage.SuspendLayout();
            this.collectionPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.countNum)).BeginInit();
            this.filterOption.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // accountLabel
            // 
            this.accountLabel.AutoSize = true;
            this.accountLabel.Location = new System.Drawing.Point(99, 17);
            this.accountLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.accountLabel.Name = "accountLabel";
            this.accountLabel.Size = new System.Drawing.Size(80, 15);
            this.accountLabel.TabIndex = 0;
            this.accountLabel.Text = "Pixiv ID/Email";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(119, 84);
            this.passwordLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(33, 15);
            this.passwordLabel.TabIndex = 1;
            this.passwordLabel.Text = "密碼";
            // 
            // UsernameBox
            // 
            this.UsernameBox.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.UsernameBox.Location = new System.Drawing.Point(10, 40);
            this.UsernameBox.Name = "UsernameBox";
            this.UsernameBox.Size = new System.Drawing.Size(262, 23);
            this.UsernameBox.TabIndex = 2;
            // 
            // PasswordBox
            // 
            this.PasswordBox.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.PasswordBox.Location = new System.Drawing.Point(10, 107);
            this.PasswordBox.Name = "PasswordBox";
            this.PasswordBox.PasswordChar = '•';
            this.PasswordBox.Size = new System.Drawing.Size(262, 23);
            this.PasswordBox.TabIndex = 3;
            // 
            // loginButton
            // 
            this.loginButton.Location = new System.Drawing.Point(59, 200);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(168, 28);
            this.loginButton.TabIndex = 4;
            this.loginButton.Text = "登入";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // accountGroup
            // 
            this.accountGroup.Controls.Add(this.accountControl);
            this.accountGroup.Location = new System.Drawing.Point(12, 12);
            this.accountGroup.Name = "accountGroup";
            this.accountGroup.Size = new System.Drawing.Size(303, 320);
            this.accountGroup.TabIndex = 5;
            this.accountGroup.TabStop = false;
            this.accountGroup.Text = "Pixiv 帳號";
            // 
            // accountControl
            // 
            this.accountControl.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.accountControl.Controls.Add(this.noLoginPage);
            this.accountControl.Controls.Add(this.loginPage);
            this.accountControl.ItemSize = new System.Drawing.Size(0, 1);
            this.accountControl.Location = new System.Drawing.Point(6, 28);
            this.accountControl.Name = "accountControl";
            this.accountControl.SelectedIndex = 0;
            this.accountControl.Size = new System.Drawing.Size(292, 243);
            this.accountControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.accountControl.TabIndex = 1;
            // 
            // noLoginPage
            // 
            this.noLoginPage.Controls.Add(this.accountLabel);
            this.noLoginPage.Controls.Add(this.UsernameBox);
            this.noLoginPage.Controls.Add(this.loginButton);
            this.noLoginPage.Controls.Add(this.passwordLabel);
            this.noLoginPage.Controls.Add(this.PasswordBox);
            this.noLoginPage.Location = new System.Drawing.Point(4, 5);
            this.noLoginPage.Name = "noLoginPage";
            this.noLoginPage.Padding = new System.Windows.Forms.Padding(3);
            this.noLoginPage.Size = new System.Drawing.Size(284, 234);
            this.noLoginPage.TabIndex = 0;
            this.noLoginPage.Text = "noLoginPage";
            this.noLoginPage.UseVisualStyleBackColor = true;
            // 
            // loginPage
            // 
            this.loginPage.Controls.Add(this.profileImg);
            this.loginPage.Controls.Add(this.logoutButton);
            this.loginPage.Controls.Add(this.accoutLabel);
            this.loginPage.Controls.Add(this.usernameLabel);
            this.loginPage.Location = new System.Drawing.Point(4, 5);
            this.loginPage.Name = "loginPage";
            this.loginPage.Padding = new System.Windows.Forms.Padding(3);
            this.loginPage.Size = new System.Drawing.Size(284, 234);
            this.loginPage.TabIndex = 1;
            this.loginPage.Text = "loginPage";
            this.loginPage.UseVisualStyleBackColor = true;
            // 
            // profileImg
            // 
            this.profileImg.Location = new System.Drawing.Point(101, 24);
            this.profileImg.Name = "profileImg";
            this.profileImg.Size = new System.Drawing.Size(75, 75);
            this.profileImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.profileImg.TabIndex = 3;
            this.profileImg.TabStop = false;
            // 
            // logoutButton
            // 
            this.logoutButton.Location = new System.Drawing.Point(76, 195);
            this.logoutButton.Name = "logoutButton";
            this.logoutButton.Size = new System.Drawing.Size(126, 33);
            this.logoutButton.TabIndex = 2;
            this.logoutButton.Text = "登出";
            this.logoutButton.UseVisualStyleBackColor = true;
            this.logoutButton.Click += new System.EventHandler(this.logoutButton_Click);
            // 
            // accoutLabel
            // 
            this.accoutLabel.Location = new System.Drawing.Point(49, 112);
            this.accoutLabel.Name = "accoutLabel";
            this.accoutLabel.Size = new System.Drawing.Size(183, 20);
            this.accoutLabel.TabIndex = 0;
            this.accoutLabel.Text = "名稱";
            this.accoutLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // usernameLabel
            // 
            this.usernameLabel.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.usernameLabel.Location = new System.Drawing.Point(50, 132);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(186, 26);
            this.usernameLabel.TabIndex = 1;
            this.usernameLabel.Text = "ID";
            this.usernameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grabOptionGroup
            // 
            this.grabOptionGroup.Controls.Add(this.grabModeControl);
            this.grabOptionGroup.Controls.Add(this.originalPictureCheck);
            this.grabOptionGroup.Controls.Add(this.countNum);
            this.grabOptionGroup.Controls.Add(this.countLabel);
            this.grabOptionGroup.Controls.Add(this.modeCombo);
            this.grabOptionGroup.Controls.Add(this.modeLabel);
            this.grabOptionGroup.Controls.Add(this.deleteCheck);
            this.grabOptionGroup.Location = new System.Drawing.Point(321, 12);
            this.grabOptionGroup.Name = "grabOptionGroup";
            this.grabOptionGroup.Size = new System.Drawing.Size(270, 175);
            this.grabOptionGroup.TabIndex = 6;
            this.grabOptionGroup.TabStop = false;
            this.grabOptionGroup.Text = "抓取選項";
            // 
            // grabModeControl
            // 
            this.grabModeControl.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.grabModeControl.Controls.Add(this.rankingPage);
            this.grabModeControl.Controls.Add(this.collectionPage);
            this.grabModeControl.Controls.Add(this.emptyPage);
            this.grabModeControl.ItemSize = new System.Drawing.Size(0, 1);
            this.grabModeControl.Location = new System.Drawing.Point(6, 116);
            this.grabModeControl.Name = "grabModeControl";
            this.grabModeControl.SelectedIndex = 0;
            this.grabModeControl.Size = new System.Drawing.Size(261, 46);
            this.grabModeControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.grabModeControl.TabIndex = 8;
            // 
            // rankingPage
            // 
            this.rankingPage.BackColor = System.Drawing.SystemColors.Control;
            this.rankingPage.Controls.Add(this.rankModeCombo);
            this.rankingPage.Controls.Add(this.rankModeLabel);
            this.rankingPage.Location = new System.Drawing.Point(4, 5);
            this.rankingPage.Name = "rankingPage";
            this.rankingPage.Padding = new System.Windows.Forms.Padding(3);
            this.rankingPage.Size = new System.Drawing.Size(253, 37);
            this.rankingPage.TabIndex = 0;
            this.rankingPage.Text = "tabPage1";
            // 
            // rankModeCombo
            // 
            this.rankModeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rankModeCombo.FormattingEnabled = true;
            this.rankModeCombo.Items.AddRange(new object[] {
            "每日",
            "每週",
            "每月"});
            this.rankModeCombo.Location = new System.Drawing.Point(124, 8);
            this.rankModeCombo.Name = "rankModeCombo";
            this.rankModeCombo.Size = new System.Drawing.Size(121, 23);
            this.rankModeCombo.TabIndex = 7;
            // 
            // rankModeLabel
            // 
            this.rankModeLabel.AutoSize = true;
            this.rankModeLabel.Location = new System.Drawing.Point(10, 11);
            this.rankModeLabel.Name = "rankModeLabel";
            this.rankModeLabel.Size = new System.Drawing.Size(72, 15);
            this.rankModeLabel.TabIndex = 6;
            this.rankModeLabel.Text = "排行榜類別";
            // 
            // collectionPage
            // 
            this.collectionPage.Controls.Add(this.privateCollection);
            this.collectionPage.Location = new System.Drawing.Point(4, 5);
            this.collectionPage.Name = "collectionPage";
            this.collectionPage.Padding = new System.Windows.Forms.Padding(3);
            this.collectionPage.Size = new System.Drawing.Size(253, 37);
            this.collectionPage.TabIndex = 1;
            this.collectionPage.Text = "tabPage2";
            this.collectionPage.UseVisualStyleBackColor = true;
            // 
            // privateCollection
            // 
            this.privateCollection.AutoSize = true;
            this.privateCollection.Location = new System.Drawing.Point(13, 9);
            this.privateCollection.Name = "privateCollection";
            this.privateCollection.Size = new System.Drawing.Size(104, 19);
            this.privateCollection.TabIndex = 0;
            this.privateCollection.Text = "抓取私人收藏";
            this.privateCollection.UseVisualStyleBackColor = true;
            // 
            // emptyPage
            // 
            this.emptyPage.Location = new System.Drawing.Point(4, 5);
            this.emptyPage.Name = "emptyPage";
            this.emptyPage.Padding = new System.Windows.Forms.Padding(3);
            this.emptyPage.Size = new System.Drawing.Size(253, 37);
            this.emptyPage.TabIndex = 2;
            this.emptyPage.Text = "tabPage1";
            this.emptyPage.UseVisualStyleBackColor = true;
            // 
            // originalPictureCheck
            // 
            this.originalPictureCheck.AutoSize = true;
            this.originalPictureCheck.Checked = true;
            this.originalPictureCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.originalPictureCheck.Location = new System.Drawing.Point(23, 62);
            this.originalPictureCheck.Name = "originalPictureCheck";
            this.originalPictureCheck.Size = new System.Drawing.Size(104, 19);
            this.originalPictureCheck.TabIndex = 2;
            this.originalPictureCheck.Text = "抓取原始圖片";
            this.originalPictureCheck.UseVisualStyleBackColor = true;
            // 
            // countNum
            // 
            this.countNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.countNum.Location = new System.Drawing.Point(134, 31);
            this.countNum.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.countNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.countNum.Name = "countNum";
            this.countNum.Size = new System.Drawing.Size(121, 23);
            this.countNum.TabIndex = 1;
            this.countNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.countNum.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // countLabel
            // 
            this.countLabel.AutoSize = true;
            this.countLabel.Location = new System.Drawing.Point(20, 33);
            this.countLabel.Name = "countLabel";
            this.countLabel.Size = new System.Drawing.Size(59, 15);
            this.countLabel.TabIndex = 0;
            this.countLabel.Text = "抓取數量";
            // 
            // modeCombo
            // 
            this.modeCombo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.modeCombo.DisplayMember = "排行榜";
            this.modeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.modeCombo.FormattingEnabled = true;
            this.modeCombo.Items.AddRange(new object[] {
            "排行榜",
            "推薦",
            "收藏"});
            this.modeCombo.Location = new System.Drawing.Point(134, 89);
            this.modeCombo.Name = "modeCombo";
            this.modeCombo.Size = new System.Drawing.Size(121, 23);
            this.modeCombo.TabIndex = 5;
            this.modeCombo.SelectedIndexChanged += new System.EventHandler(this.modeCombo_SelectedIndexChanged);
            // 
            // modeLabel
            // 
            this.modeLabel.AutoSize = true;
            this.modeLabel.Location = new System.Drawing.Point(20, 92);
            this.modeLabel.Name = "modeLabel";
            this.modeLabel.Size = new System.Drawing.Size(59, 15);
            this.modeLabel.TabIndex = 4;
            this.modeLabel.Text = "抓取模式";
            // 
            // deleteCheck
            // 
            this.deleteCheck.AutoSize = true;
            this.deleteCheck.Checked = true;
            this.deleteCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.deleteCheck.Location = new System.Drawing.Point(134, 62);
            this.deleteCheck.Name = "deleteCheck";
            this.deleteCheck.Size = new System.Drawing.Size(104, 19);
            this.deleteCheck.TabIndex = 3;
            this.deleteCheck.Text = "清除歷史圖片";
            this.deleteCheck.UseVisualStyleBackColor = true;
            // 
            // filterOption
            // 
            this.filterOption.Controls.Add(this.numericUpDown3);
            this.filterOption.Controls.Add(this.collectionLabel);
            this.filterOption.Controls.Add(this.numericUpDown2);
            this.filterOption.Controls.Add(this.viewCountLabel);
            this.filterOption.Controls.Add(this.numericUpDown1);
            this.filterOption.Controls.Add(this.resolution);
            this.filterOption.Controls.Add(this.paintingCheck);
            this.filterOption.Controls.Add(this.R18Check);
            this.filterOption.Location = new System.Drawing.Point(322, 193);
            this.filterOption.Name = "filterOption";
            this.filterOption.Size = new System.Drawing.Size(269, 139);
            this.filterOption.TabIndex = 7;
            this.filterOption.TabStop = false;
            this.filterOption.Text = "過濾選項";
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown3.Location = new System.Drawing.Point(133, 103);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(119, 23);
            this.numericUpDown3.TabIndex = 7;
            this.numericUpDown3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // collectionLabel
            // 
            this.collectionLabel.AutoSize = true;
            this.collectionLabel.Location = new System.Drawing.Point(17, 105);
            this.collectionLabel.Name = "collectionLabel";
            this.collectionLabel.Size = new System.Drawing.Size(46, 15);
            this.collectionLabel.TabIndex = 6;
            this.collectionLabel.Text = "收藏數";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown2.Location = new System.Drawing.Point(133, 76);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(119, 23);
            this.numericUpDown2.TabIndex = 5;
            this.numericUpDown2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // viewCountLabel
            // 
            this.viewCountLabel.AutoSize = true;
            this.viewCountLabel.Location = new System.Drawing.Point(17, 78);
            this.viewCountLabel.Name = "viewCountLabel";
            this.viewCountLabel.Size = new System.Drawing.Size(46, 15);
            this.viewCountLabel.TabIndex = 4;
            this.viewCountLabel.Text = "瀏覽數";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown1.Location = new System.Drawing.Point(133, 48);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(119, 23);
            this.numericUpDown1.TabIndex = 3;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown1.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // resolution
            // 
            this.resolution.AutoSize = true;
            this.resolution.Location = new System.Drawing.Point(17, 50);
            this.resolution.Name = "resolution";
            this.resolution.Size = new System.Drawing.Size(72, 15);
            this.resolution.TabIndex = 2;
            this.resolution.Text = "最小解析度";
            // 
            // paintingCheck
            // 
            this.paintingCheck.AutoSize = true;
            this.paintingCheck.Location = new System.Drawing.Point(133, 22);
            this.paintingCheck.Name = "paintingCheck";
            this.paintingCheck.Size = new System.Drawing.Size(78, 19);
            this.paintingCheck.TabIndex = 1;
            this.paintingCheck.Text = "過濾插畫";
            this.paintingCheck.UseVisualStyleBackColor = true;
            // 
            // R18Check
            // 
            this.R18Check.AutoSize = true;
            this.R18Check.Location = new System.Drawing.Point(22, 22);
            this.R18Check.Name = "R18Check";
            this.R18Check.Size = new System.Drawing.Size(103, 19);
            this.R18Check.TabIndex = 0;
            this.R18Check.Text = "抓取 R18 圖片";
            this.R18Check.UseVisualStyleBackColor = true;
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 343);
            this.Controls.Add(this.filterOption);
            this.Controls.Add(this.grabOptionGroup);
            this.Controls.Add(this.accountGroup);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingForm";
            this.Text = "設定";
            this.Load += new System.EventHandler(this.SettingForm_Load);
            this.accountGroup.ResumeLayout(false);
            this.accountControl.ResumeLayout(false);
            this.noLoginPage.ResumeLayout(false);
            this.noLoginPage.PerformLayout();
            this.loginPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.profileImg)).EndInit();
            this.grabOptionGroup.ResumeLayout(false);
            this.grabOptionGroup.PerformLayout();
            this.grabModeControl.ResumeLayout(false);
            this.rankingPage.ResumeLayout(false);
            this.rankingPage.PerformLayout();
            this.collectionPage.ResumeLayout(false);
            this.collectionPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.countNum)).EndInit();
            this.filterOption.ResumeLayout(false);
            this.filterOption.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label accountLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox UsernameBox;
        private System.Windows.Forms.TextBox PasswordBox;
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.GroupBox accountGroup;
        private System.Windows.Forms.GroupBox grabOptionGroup;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Label accoutLabel;
        private System.Windows.Forms.Button logoutButton;
        private System.Windows.Forms.PictureBox profileImg;
        private System.Windows.Forms.TabControl accountControl;
        private System.Windows.Forms.TabPage noLoginPage;
        private System.Windows.Forms.TabPage loginPage;
        private System.Windows.Forms.NumericUpDown countNum;
        private System.Windows.Forms.Label countLabel;
        private System.Windows.Forms.CheckBox originalPictureCheck;
        private System.Windows.Forms.ComboBox rankModeCombo;
        private System.Windows.Forms.Label rankModeLabel;
        private System.Windows.Forms.ComboBox modeCombo;
        private System.Windows.Forms.Label modeLabel;
        private System.Windows.Forms.CheckBox deleteCheck;
        private System.Windows.Forms.TabControl grabModeControl;
        private System.Windows.Forms.TabPage rankingPage;
        private System.Windows.Forms.TabPage collectionPage;
        private System.Windows.Forms.CheckBox privateCollection;
        private System.Windows.Forms.TabPage emptyPage;
        private System.Windows.Forms.GroupBox filterOption;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.Label collectionLabel;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label viewCountLabel;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label resolution;
        private System.Windows.Forms.CheckBox paintingCheck;
        private System.Windows.Forms.CheckBox R18Check;
    }
}