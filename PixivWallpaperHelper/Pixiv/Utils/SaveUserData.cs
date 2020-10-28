using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Pixiv.Objects;

namespace WindowsFormsApp1.Pixiv.Utils
{
    public class SaveUserData
    {
        public static void SaveAuthData(Authorize authorize = null)
        {
            Properties.Settings.Default.KEY_PIXIV_ACCESS_TOKEN = authorize?.AccessToken;
            Properties.Settings.Default.KEY_PIXIV_REFRESH_TOKEN = authorize?.RefreshToken;
            Properties.Settings.Default.KEY_PIXIV_DEVICE_TOKEN = "pixiv";
            Properties.Settings.Default.KEY_PIXIV_USER_ID = authorize?.User.Id;
            Properties.Settings.Default.KEY_PIXIV_USER_USERNAME = authorize?.User.Account;
            Properties.Settings.Default.KEY_PIXIV_USER_NAME = authorize?.User.Name;
            Properties.Settings.Default.KEY_PIXIV_USER_IMG = authorize?.User.ProfileImageUrls.Px170x170;
            Properties.Settings.Default.Save();
        }

        public static void ClearAuthData()
        {
            SaveAuthData();
        }
    }
}
