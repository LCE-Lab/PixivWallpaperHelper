using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PixivWallpaperHelper.Pixiv.OAuth;
using PixivWallpaperHelper.Utils;
using PixivWallpaperHelper.Pixiv.Objects;
using PixivWallpaperHelper.Pixiv.Mode;

namespace PixivWallpaperHelper.Pixiv.OAuth
{
    public enum WallPaperInfoStatus { SUCCESS, NOTFOUND, PATHINVALID }

    class WallpaperFetcher
    {
        private static string path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\PixivWallpapers";
        private static LocalArtworksHelper localArtworksHelper;
        public WallpaperFetcher()
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            localArtworksHelper = new LocalArtworksHelper();
        }

        public WallPaperInfoStatus getWallpaperInfo(string wallpaperPath, out LocalArtwork value)
        {
            if (Path.GetDirectoryName(wallpaperPath).Equals(path))
            {
                return localArtworksHelper.getArtworkInfo(Path.GetFileNameWithoutExtension(wallpaperPath), out value)
                    ? WallPaperInfoStatus.SUCCESS
                    : WallPaperInfoStatus.NOTFOUND;
            }
            else
            {
                value = new LocalArtwork();
                return WallPaperInfoStatus.PATHINVALID;
            }
        }

        public async void fetchWallpaper() {
            IllustList illustList;
            if (!Properties.Auth.Default.KEY_PIXIV_USER_LOGIN) {
                illustList = await Fallback.GetFallback();

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
                            var success = localArtworksHelper.AddAndSaveArtwork(
                                    Image.SaveImage(url),
                                    finalPath,
                                    result.Title,
                                    result.User.Name,
                                    $"{result.Id}_{i}",
                                    url
                                );
                            if (success) { step++; }
                        }
                    }
                    else
                    {
                        var url = (originalImage) ? result.MetaSinglePage.OriginalImageUrl : result.ImageUrls.Large;
                        var finalPath = $"{path}\\{result.Id}{Path.GetExtension(url)}";
                        var success = localArtworksHelper.AddAndSaveArtwork(
                                Image.SaveImage(url),
                                finalPath,
                                result.Title,
                                result.User.Name,
                                result.Id.ToString(),
                                url
                            );
                        if (success) { step++; }
                    }
                    
                }
                // End TODO
            }
            localArtworksHelper.Save();
        }
    }
}
