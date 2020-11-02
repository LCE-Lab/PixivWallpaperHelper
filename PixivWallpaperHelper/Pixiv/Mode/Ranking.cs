using Newtonsoft.Json;
using PixivWallpaperHelper.Pixiv.Objects;
using PixivWallpaperHelper.Pixiv.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PixivWallpaperHelper.Pixiv.Mode
{
    public enum Category
    {
        Daily = 0,
        Weekly = 1,
        Monthly = 2
    }
    public class Ranking
    {
        private static string API = "https://app-api.pixiv.net/v1/illust/ranking";
        public static async Task<List<Illust>> GetRanking(Category category, string accessToken, decimal count = 0)
        {
            var list = new List<Illust>();
            string type;
            switch (category) {
                case Category.Daily:
                    type = "day";
                    break;
                case Category.Weekly:
                    type = "week";
                    break;
                case Category.Monthly:
                    type = "month";
                    break;
                default:
                    type = "day";
                    break;
            }

            var param = new Dictionary<string, string>
            {
                { "mode", type },
            };

            var headers = new Dictionary<string, string>
            {
                { "Authorization", "Bearer " + accessToken } ,
            };

            var json = await (await Request.CreateRequest(MethodType.GET, API, param, headers)).GetResponseStringAsync();
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
