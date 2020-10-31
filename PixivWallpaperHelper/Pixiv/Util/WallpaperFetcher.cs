using System;
using System.IO;
using PixivWallpaperHelper.Pixiv.OAuth;

namespace PixivWallpaperHelper.Pixiv.Util
{
    internal class WallpaperFetcher
    {
        private static readonly string path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) +
                                              "\\PixivWallpapers";

        public WallpaperFetcher()
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        }

        public static async void FetchWallpaper()
        {
            if (Properties.Auth.Default.KEY_PIXIV_USER_LOGIN) return;
            var fallbackResult = await PublicApi.Fallback();
            var count = Properties.Settings.Default.countNum;
            var step = 0;

            foreach (var result in fallbackResult.Illusts)
            {
                if (step == count) break;
                Console.WriteLine(result.Id);
                step++;
            }
        }
    }
}
