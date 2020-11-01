using Newtonsoft.Json;
using PixivWallpaperHelper.Pixiv.Objects;
using PixivWallpaperHelper.Pixiv.Util;
using PixivWallpaperHelper.Pixiv.Utils;
using PixivWallpaperHelper.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PixivWallpaperHelper.Pixiv.OAuth
{
    public class Auth
    {
        private static readonly string API = "https://oauth.secure.pixiv.net/auth/token";
        private static readonly string clientID = "MOBrBDS8blbauoSck0ZfDbtuzpyT";
        private static readonly string clientSecret = "lsACyCD94FhDUtGTXi3QzcFE2uU1hqtDaKeqrdwj";
        private static readonly string hashSecret = "28c1fdd170a5204386cb1313c7077b34f83e4aaf4aa829ce78c231e05b0bae2c";
        public async static Task<Authorize> login(string username, string password)
        {
            var param = new Dictionary<string, string>
            {
                { "username", username },
                { "password", password },
                { "grant_type", "password" },
                { "device_token", "pixiv" },
                { "get_secure_url", "true" },
                { "client_id", clientID },
                { "client_secret", clientSecret },
            };

            var LocalTime = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Local).ToString("yyyy'-'MM'-'dd'T'HH':'mm':'sszzz");
            var hash = MD5.MD5code(LocalTime + hashSecret);

            var headers = new Dictionary<string, string>
            {
                { "Accept-Language", "en_US" } ,
                { "X-Client-Time",  LocalTime} ,
                { "X-Client-Hash", hash } ,
            };

            var json = await (await Request.CreateRequest(Util.MethodType.POST, API, param, headers)).GetResponseStringAsync();
            var authorize = JsonConvert.DeserializeObject<Authorize>(json);

            Data.SaveAuthData(authorize);

            return authorize;
        }
    }
}
