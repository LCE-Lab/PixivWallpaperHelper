using System.Drawing;
using System.IO;
using System.Net;

namespace PixivWallpaperHelper.Utils
{
    public class Image
    {
        public static Bitmap SaveImage(string url)
        {
            WebClient client = new WebClient();
            client.Headers["Referer"] = "https://www.pixiv.net";
            Stream stream = client.OpenRead(url);
            Bitmap bitmap = new Bitmap(stream);

            stream.Flush();
            stream.Close();
            client.Dispose();

            return bitmap;
        }
    }
}
