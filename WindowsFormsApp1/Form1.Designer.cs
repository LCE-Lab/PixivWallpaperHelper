namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.fastTimer = new System.Windows.Forms.Timer(this.components);
            this.refreshBtn = new System.Windows.Forms.Button();
            this.currentWallpaper = new System.Windows.Forms.PictureBox();
            this.wallpaperRefreshTimer = new System.Windows.Forms.Timer(this.components);
            this.titleLabel = new System.Windows.Forms.LinkLabel();
            this.authorLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.currentWallpaper)).BeginInit();
            this.SuspendLayout();
            // 
            // fastTimer
            // 
            this.fastTimer.Enabled = true;
            this.fastTimer.Tick += new System.EventHandler(this.fastTimer_Tick);
            // 
            // refreshBtn
            // 
            this.refreshBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshBtn.BackColor = System.Drawing.Color.Transparent;
            this.refreshBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.refreshBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.refreshBtn.Location = new System.Drawing.Point(630, 341);
            this.refreshBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.refreshBtn.Name = "refreshBtn";
            this.refreshBtn.Size = new System.Drawing.Size(97, 62);
            this.refreshBtn.TabIndex = 1;
            this.refreshBtn.Text = "重新整理";
            this.refreshBtn.UseVisualStyleBackColor = false;
            this.refreshBtn.Click += new System.EventHandler(this.refreshBtn_Click);
            // 
            // currentWallpaper
            // 
            this.currentWallpaper.Location = new System.Drawing.Point(0, 0);
            this.currentWallpaper.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.currentWallpaper.Name = "currentWallpaper";
            this.currentWallpaper.Size = new System.Drawing.Size(728, 403);
            this.currentWallpaper.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.currentWallpaper.TabIndex = 2;
            this.currentWallpaper.TabStop = false;
            // 
            // wallpaperRefreshTimer
            // 
            this.wallpaperRefreshTimer.Enabled = true;
            this.wallpaperRefreshTimer.Interval = 5000;
            this.wallpaperRefreshTimer.Tick += new System.EventHandler(this.wallpaperRefreshTimer_Tick);
            // 
            // titleLabel
            // 
            this.titleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.titleLabel.AutoSize = true;
            this.titleLabel.BackColor = System.Drawing.Color.Black;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.ForeColor = System.Drawing.Color.White;
            this.titleLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.titleLabel.LinkColor = System.Drawing.Color.White;
            this.titleLabel.Location = new System.Drawing.Point(12, 341);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(143, 30);
            this.titleLabel.TabIndex = 3;
            this.titleLabel.TabStop = true;
            this.titleLabel.Text = "Sample Title 1";
            this.titleLabel.VisitedLinkColor = System.Drawing.Color.White;
            // 
            // authorLabel
            // 
            this.authorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.authorLabel.AutoSize = true;
            this.authorLabel.BackColor = System.Drawing.Color.Black;
            this.authorLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.authorLabel.ForeColor = System.Drawing.Color.White;
            this.authorLabel.Location = new System.Drawing.Point(12, 375);
            this.authorLabel.Name = "authorLabel";
            this.authorLabel.Size = new System.Drawing.Size(105, 17);
            this.authorLabel.TabIndex = 4;
            this.authorLabel.Text = "Sample Author 1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 401);
            this.Controls.Add(this.authorLabel);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.refreshBtn);
            this.Controls.Add(this.currentWallpaper);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Pixiv Wallpaper Helper";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.currentWallpaper)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer fastTimer;
        private System.Windows.Forms.Button refreshBtn;
        private System.Windows.Forms.PictureBox currentWallpaper;
        private System.Windows.Forms.Timer wallpaperRefreshTimer;
        private System.Windows.Forms.LinkLabel titleLabel;
        private System.Windows.Forms.Label authorLabel;
    }
}

