using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using PixivWallpaperHelper.Utils;
using PixivWallpaperHelper.Pixiv.Objects;
using PixivWallpaperHelper.Pixiv.Mode;
using static PixivWallpaperHelper.Pixiv.Objects.IllustTypes;

namespace PixivWallpaperHelper.Pixiv.OAuth
{
    public enum WallPaperInfoStatus { Success, NotFound, PathInvalid }

    internal class WallpaperFetcher
    {
        private static readonly string Path = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)}\\PixivWallpapers";
        private static LocalArtworksHelper LocalArtworksHelper;
        public WallpaperFetcher()
        {
            if (!Directory.Exists(Path))
            {
                _ = Directory.CreateDirectory(Path);
            }

            LocalArtworksHelper = new LocalArtworksHelper();
        }

        public WallPaperInfoStatus GetWallpaperInfoFromWallpaper(string wallpaperPath, out LocalArtwork value)
        {
            if (System.IO.Path.GetDirectoryName(wallpaperPath).Equals(Path))
            {
                if (LocalArtworksHelper.GetArtworkInfo(System.IO.Path.GetFileNameWithoutExtension(wallpaperPath), out value)) 
                {
                    _ = LocalArtworksHelper.SetCurrentWallpaper(System.IO.Path.GetFileNameWithoutExtension(wallpaperPath));
                    LocalArtworksHelper.Save();
                    return WallPaperInfoStatus.Success;
                }
                LocalArtworksHelper.UnsetLastCurrent();
                return WallPaperInfoStatus.NotFound;
            }
            else
            {
                value = new LocalArtwork();
                LocalArtworksHelper.UnsetLastCurrent();
                return WallPaperInfoStatus.PathInvalid;
            }
        }

        public void ClearCurrentWallpaperMark()
        {
            LocalArtworksHelper.UnsetLastCurrent();
        }

        public bool IsLocalUnchangedWallpaperEmpty()
        {
            return LocalArtworksHelper.GetUnchangedWallpaperCount() <= 0;
        }

        public async void FetchWallpaper()
        {
            List<Illust> illustList;

            decimal count = Properties.Settings.Default.countNum;
            bool fetchNsfw = Properties.Settings.Default.R18Check;
            bool filterPanting = Properties.Settings.Default.paintingCheck;
            decimal filterResolution = Properties.Settings.Default.resolutionNum;
            bool originalImage = (filterResolution > 1200) || Properties.Settings.Default.originPictureCheck;
            decimal minView = Properties.Settings.Default.viewCountNum;
            decimal minCollection = Properties.Settings.Default.collectionNum;
            string fetchMode = Properties.Settings.Default.modeCombo;
            string rankFetchCategory = Properties.Settings.Default.rankModeCombo;
            bool privateMode = Properties.Settings.Default.privateColletcion;

            if (!Properties.Auth.Default.KEY_PIXIV_USER_LOGIN)
            {
                IllustList list = await Fallback.GetFallback();
                illustList = list.Illusts.ToList();
            } 
            else
            {
                _ = await Auth.Refresh(Properties.Auth.Default.KEY_PIXIV_DEVICE_TOKEN, Properties.Auth.Default.KEY_PIXIV_REFRESH_TOKEN);

                string accessToken = Properties.Auth.Default.KEY_PIXIV_ACCESS_TOKEN;
                long userID = Properties.Auth.Default.KEY_PIXIV_USER_ID;
                Category rankMode;

                switch (rankFetchCategory)
                {
                    case "每日":
                        rankMode = Category.Daily;
                        break;
                    case "每週":
                        rankMode = Category.Weekly;
                        break;
                    case "每月":
                        rankMode = Category.Monthly;
                        break;
                    default:
                        rankMode = Category.Daily;
                        break;
                }

                switch (fetchMode)
                {
                    case "排行榜":
                        illustList = await Ranking.GetRanking(rankMode, accessToken);
                        break;
                    case "推薦":
                        illustList = await Recommend.GetRecommend(accessToken);
                        break;
                    case "收藏":
                        illustList = await Bookmark.GetBookmark(accessToken, userID, privateMode);
                        break;
                    default:
                        illustList = await Ranking.GetRanking(rankMode, accessToken);
                        break;
                }
            }

            foreach (Illust result in illustList)
            {
                if (LocalArtworksHelper.GetUnchangedWallpaperCount() >= count) { break; }
                if (!fetchNsfw && result.SanityLevel >= 4) { continue; }
                if (filterPanting && result.Type != TypeEnum.Illust) { continue; }
                if (minView > result.TotalView || minCollection > result.TotalBookmarks) { continue; }

                if (result.PageCount > 1)
                {
                    for (int i = 0; i < result.MetaPages.Length; i++)
                    {
                        if (LocalArtworksHelper.GetUnchangedWallpaperCount() >= count) { break; }
                        string url = originalImage ? result.MetaPages[i].ImageUrls.Original : result.MetaPages[i].ImageUrls.Large;
                        string finalPath = $"{Path}\\{result.Id}_{i}{System.IO.Path.GetExtension(url)}";
                        _ = LocalArtworksHelper.AddAndSaveArtwork(
                                Image.SaveImage(url),
                                finalPath,
                                result.Title,
                                result.User.Name,
                                $"{result.Id}_{i}",
                                url
                            );
                    }
                }
                else
                {
                    string url = originalImage ? result.MetaSinglePage.OriginalImageUrl : result.ImageUrls.Large;
                    string finalPath = $"{Path}\\{result.Id}{System.IO.Path.GetExtension(url)}";
                    _ = LocalArtworksHelper.AddAndSaveArtwork(
                            Image.SaveImage(url),
                            finalPath,
                            result.Title,
                            result.User.Name,
                            result.Id.ToString(),
                            url
                        );
                }
            }

            if (Properties.Settings.Default.deleteCheck) { LocalArtworksHelper.ClearChangedWallpaper(); }
            LocalArtworksHelper.Save();
        }
    }
}
