using System.Drawing;
using System.IO;
using System.Net;

namespace PixivWallpaperHelper.Utils
{    public static class Image
    {
        public static Bitmap SaveImage(string url, string path = null)
        {
            var client = new WebClient();
            client.Headers["Referer"] = "https://www.pixiv.net";
            var stream = client.OpenRead(url);
            var bitmap = new Bitmap(stream);

            if (path != null)
            {
                var fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                stream.CopyTo(fileStream);
            }

            stream.Flush();
            stream.Close();
            client.Dispose();

            return bitmap;
        }
    }
}
