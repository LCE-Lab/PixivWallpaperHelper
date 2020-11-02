using Newtonsoft.Json;
using PixivWallpaperHelper.Pixiv.Objects;
using PixivWallpaperHelper.Pixiv.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixivWallpaperHelper.Pixiv.Mode
{
    public class Bookmark
    {
        private static string API = "https://app-api.pixiv.net/v1/user/bookmarks/illust";
        public static async Task<List<Illust>> GetBookmark(string accessToken, long userID, bool privateMode)
        {
            var list = new List<Illust>();
            var type = privateMode ? "private" : "public";
            var i = 0;

            var param = new Dictionary<string, string>
            {
                { "restrict", type },
                { "user_id", userID.ToString() } ,
            };

            var headers = new Dictionary<string, string>
            {
                { "Authorization", "Bearer " + accessToken } ,
            };

            do
            {
                var json = await (await Request.CreateRequest(MethodType.GET, API, param, headers)).GetResponseStringAsync();
                var ranking = JsonConvert.DeserializeObject<IllustList>(json);

                if (ranking.NextUrl != null)
                {
                    API = ranking.NextUrl;
                }
                else
                {
                    if (privateMode|| i >= 3) break;

                    i++;
                }

                list.AddRange(ranking.Illusts);
            } while (list.Count < Properties.Settings.Default.countNum);

            return list;
        }
    }
}
