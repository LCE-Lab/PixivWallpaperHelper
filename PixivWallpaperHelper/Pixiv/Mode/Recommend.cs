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
    public class Recommend
    {
        private static string API = "https://app-api.pixiv.net/v1/illust/recommended";
        public static async Task<List<Illust>> GetRecommend(string accessToken, decimal count= 0)
        {
            var list = new List<Illust>();

            var headers = new Dictionary<string, string>
            {
                { "Authorization", "Bearer " + accessToken } ,
            };

            var json = await (await Request.CreateRequest(MethodType.GET, API, null, headers)).GetResponseStringAsync();
            var ranking = JsonConvert.DeserializeObject<IllustList>(json);

            do
            {
                if (ranking.NextUrl != null) API = ranking.NextUrl; else break;

                list.AddRange(ranking.Illusts);
            } while (list.Count < count);

            return list;
        }
    }
}
