using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using PixivWallpaperHelper.Pixiv.Objects;

namespace PixivWallpaperHelper.Pixiv.Utils
{
    public class SaveUserData
    {
        public static void SaveAuthData(Authorize authorize = null)
        {
            Properties.Auth.Default.KEY_PIXIV_ACCESS_TOKEN = authorize == null ? "" : authorize.AccessToken;
            Properties.Auth.Default.KEY_PIXIV_REFRESH_TOKEN = authorize == null ? "" : authorize.RefreshToken;
            Properties.Auth.Default.KEY_PIXIV_DEVICE_TOKEN = "pixiv";
            Properties.Auth.Default.KEY_PIXIV_USER_ID = (long)(authorize == null ? 0 : authorize.User.Id);
            Properties.Auth.Default.KEY_PIXIV_USER_USERNAME = authorize == null ? "" : authorize.User.Account;
            Properties.Auth.Default.KEY_PIXIV_USER_NAME = authorize == null ? "" : authorize.User.Name;
            Properties.Auth.Default.KEY_PIXIV_USER_IMG = authorize == null ? "" : authorize.User.ProfileImageUrls.Px170x170;
            Properties.Auth.Default.Save();
        }

        public static void ClearAuthData()
        {
            SaveAuthData();
        }
    }
}
