using System;
using System.Text;

namespace PixivWallpaperHelper.Pixiv.Util
{
    public static class Md5
    {
        /// <summary>
        /// Create MD5 String
        /// </summary>
        public static string Md5Code(string str)
        {
            var md5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
            var data = md5Hasher.ComputeHash((new ASCIIEncoding()).GetBytes(str));
            var sBuilder = new StringBuilder();
            foreach (var t in data)
            {
                sBuilder.Append(t.ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
