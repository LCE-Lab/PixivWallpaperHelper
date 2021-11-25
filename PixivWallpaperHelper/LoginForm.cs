using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace PixivWallpaperHelper
{
    public partial class LoginForm : Form
    {
        public string Code { get; private set; }

        public LoginForm()
        {
            InitializeComponent();
        }

        public void SetUrl(string url)
        {
            webView21.Source = new System.Uri(url, System.UriKind.Absolute);
        }

        private void webView21_NavigationStarting(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs e)
        {
            if (e.Uri.StartsWith(@"pixiv://"))
            {
                Uri uri = new Uri(e.Uri);
                Code = HttpUtility.ParseQueryString(uri.Query).Get("code");
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
