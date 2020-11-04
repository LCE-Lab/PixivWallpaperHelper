using System.Text;
using System.Security.Cryptography;

namespace PixivWallpaperHelper.Pixiv.Utils
{
    public static class MD5
    {
        /// <summary>
        /// Create MD5 String
        /// </summary>
        public static string MD5Code(string str)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(new ASCIIEncoding().GetBytes(str));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                _ = sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
