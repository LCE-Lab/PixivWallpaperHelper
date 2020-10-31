using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixivWallpaperHelper.Pixiv.OAuth;

namespace PixivWallpaperHelper.Pixiv.OAuth
{
    class WallpaperFetcher
    {
        private static string path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\PixivWallpapers";
        public WallpaperFetcher()
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        }

        public async void fetchWallpaper() {
            if (!Properties.Auth.Default.KEY_PIXIV_USER_LOGIN) {
                var fallbackResult = await PublicAPI.Fallback();
                decimal count = Properties.Settings.Default.countNum;
                int step = 0;
                
                foreach (var result in fallbackResult.Illusts)
                {
                    if (step == count) break;
                    Console.WriteLine(result.Id);
                    step++;
                }
            }
        }
    }
}
