namespace WindowsFormsApp1
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
            this.loginGroup = new System.Windows.Forms.GroupBox();
            this.loginBtn = new System.Windows.Forms.Button();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.accountLabel = new System.Windows.Forms.Label();
            this.accountBox = new System.Windows.Forms.TextBox();
            this.loginGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // loginGroup
            // 
            this.loginGroup.Controls.Add(this.loginBtn);
            this.loginGroup.Controls.Add(this.passwordLabel);
            this.loginGroup.Controls.Add(this.passwordBox);
            this.loginGroup.Controls.Add(this.accountLabel);
            this.loginGroup.Controls.Add(this.accountBox);
            this.loginGroup.Location = new System.Drawing.Point(12, 13);
            this.loginGroup.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.loginGroup.Name = "loginGroup";
            this.loginGroup.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.loginGroup.Size = new System.Drawing.Size(197, 109);
            this.loginGroup.TabIndex = 0;
            this.loginGroup.TabStop = false;
            this.loginGroup.Text = "登入";
            // 
            // loginBtn
            // 
            this.loginBtn.Location = new System.Drawing.Point(64, 81);
            this.loginBtn.Name = "loginBtn";
            this.loginBtn.Size = new System.Drawing.Size(75, 23);
            this.loginBtn.TabIndex = 4;
            this.loginBtn.Text = "登入";
            this.loginBtn.UseVisualStyleBackColor = true;
            this.loginBtn.Click += new System.EventHandler(this.loginBtn_Click);
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(16, 54);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(33, 15);
            this.passwordLabel.TabIndex = 3;
            this.passwordLabel.Text = "密碼";
            // 
            // passwordBox
            // 
            this.passwordBox.Location = new System.Drawing.Point(64, 51);
            this.passwordBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.PasswordChar = '*';
            this.passwordBox.Size = new System.Drawing.Size(116, 23);
            this.passwordBox.TabIndex = 2;
            // 
            // accountLabel
            // 
            this.accountLabel.AutoSize = true;
            this.accountLabel.Location = new System.Drawing.Point(16, 23);
            this.accountLabel.Name = "accountLabel";
            this.accountLabel.Size = new System.Drawing.Size(33, 15);
            this.accountLabel.TabIndex = 1;
            this.accountLabel.Text = "帳號";
            // 
            // accountBox
            // 
            this.accountBox.Location = new System.Drawing.Point(64, 20);
            this.accountBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.accountBox.Name = "accountBox";
            this.accountBox.Size = new System.Drawing.Size(116, 23);
            this.accountBox.TabIndex = 0;
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 321);
            this.Controls.Add(this.loginGroup);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingForm";
            this.Text = "設定";
            this.loginGroup.ResumeLayout(false);
            this.loginGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox loginGroup;
        private System.Windows.Forms.Label accountLabel;
        private System.Windows.Forms.TextBox accountBox;
        private System.Windows.Forms.Button loginBtn;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox passwordBox;
    }
}