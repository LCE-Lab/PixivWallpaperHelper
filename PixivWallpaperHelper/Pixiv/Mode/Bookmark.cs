using Newtonsoft.Json;
using PixivWallpaperHelper.Pixiv.Objects;
using PixivWallpaperHelper.Pixiv.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PixivWallpaperHelper.Pixiv.Mode
{
    public class Bookmark
    {
        public static async Task<List<Illust>> GetBookmark(string accessToken, long userID, bool privateMode)
        {
            string PublicAPI = "https://app-api.pixiv.net/v1/user/bookmarks/illust?restrict=public&user_id=";
            string PrivateAPI = "https://app-api.pixiv.net/v1/user/bookmarks/illust?restrict=private&user_id=";
            List<Illust> list = new List<Illust>();
            int i = 0;
            string API = privateMode ? PrivateAPI + $"{userID}" : PublicAPI + $"{userID}";

            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Authorization", "Bearer " + accessToken } ,
            };

            do
            {
                string json = await (await Request.CreateRequest(MethodType.GET, API, null, headers)).GetResponseStringAsync();
                IllustList ranking = JsonConvert.DeserializeObject<IllustList>(json);

                if (ranking.NextUrl != null)
                {
                    API = ranking.NextUrl;
                }
                else
                {
                    if (privateMode || i >= 3) { break; };

                    i++;
                }

                list.AddRange(ranking.Illusts);
            } while (list.Count < Properties.Settings.Default.countNum);

            return list;
        }
    }
}
