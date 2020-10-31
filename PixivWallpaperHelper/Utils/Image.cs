using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PixivWallpaperHelper.Utils
{    public class Image
    {
        public static Bitmap SaveImage(string url, string path = null)
        {
            WebClient client = new WebClient();
            client.Headers["Referer"] = "https://www.pixiv.net";
            Stream stream = client.OpenRead(url);
            Bitmap bitmap = new Bitmap(stream);

            if (path != null)
            {
                FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                stream.CopyTo(fileStream);
            }

            stream.Flush();
            stream.Close();
            client.Dispose();

            return bitmap;
        }
    }
}
