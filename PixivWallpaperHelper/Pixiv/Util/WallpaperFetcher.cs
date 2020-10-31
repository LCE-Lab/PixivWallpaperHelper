using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixivWallpaperHelper.Pixiv.OAuth;
using PixivWallpaperHelper.Utils;
using PixivWallpaperHelper.Pixiv.Objects;

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
            IllustList illustList;
            if (!Properties.Auth.Default.KEY_PIXIV_USER_LOGIN) {
                illustList = await PublicAPI.Fallback();

                // TODO Move out of if
                decimal count = Properties.Settings.Default.countNum;
                var fetchNsfw = Properties.Settings.Default.R18Check;
                var filterPanting = Properties.Settings.Default.paintingCheck;
                var filterResolution = Properties.Settings.Default.resolutionNum;
                var originalImage = (filterResolution > 1200) ? true : Properties.Settings.Default.originPictureCheck;
                var minView = Properties.Settings.Default.viewCountNum;
                var minCollection = Properties.Settings.Default.collectionNum;
                int step = 0;

                foreach (var result in illustList.Illusts)
                {
                    if (step == count) break;
                    if (!fetchNsfw && result.SanityLevel >= 4) continue;
                    if (filterPanting && result.Type != TypeEnum.Illust) continue;
                    if (minView > result.TotalView || minCollection > result.TotalBookmarks) continue;

                    if (result.PageCount > 1)
                    {
                        for (int i = 0; i < result.MetaPages.Length; i++)
                        {
                            Page page = result.MetaPages[i];
                            var url = (originalImage) ? page.ImageUrls.Original : page.ImageUrls.Large;
                            var finalPath = $"{path}\\{result.Id}_{i}{Path.GetExtension(url)}";
                            Image.SaveImage(url, finalPath);
                            // TODO Save artwork infomation (title author url etc)
                        }
                    }
                    else
                    {
                        var url = (originalImage) ? result.MetaSinglePage.OriginalImageUrl : result.ImageUrls.Large;
                        var finalPath = $"{path}\\{result.Id}{Path.GetExtension(url)}";
                        Image.SaveImage(url, finalPath);
                        // TODO Save artwork infomation (title author url etc)
                    }
                    step++;
                }
                // End TODO
            }
        }
    }
}
