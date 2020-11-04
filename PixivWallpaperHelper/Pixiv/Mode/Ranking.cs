using Newtonsoft.Json;
using PixivWallpaperHelper.Pixiv.Objects;
using PixivWallpaperHelper.Pixiv.Util;
using System.Collections.Generic;
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
        public static async Task<List<Illust>> GetRanking(Category category, string accessToken)
        {
            string API = "https://app-api.pixiv.net/v1/illust/ranking?mode=";
            List<Illust> list = new List<Illust>();

            switch (category)
            {
                case Category.Daily:
                    API += "day";
                    break;
                case Category.Weekly:
                    API += "week";
                    break;
                case Category.Monthly:
                    API += "month";
                    break;
                default:
                    API += "day";
                    break;
            }

            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Authorization", "Bearer " + accessToken } ,
            };

            do
            {
                string json = await (await Request.CreateRequest(MethodType.GET, API, null, headers)).GetResponseStringAsync();
                IllustList ranking = JsonConvert.DeserializeObject<IllustList>(json);

                if (ranking.NextUrl != null) { API = ranking.NextUrl; } else { break; }

                list.AddRange(ranking.Illusts);
            } while (list.Count < Properties.Settings.Default.countNum);

            return list;
        }
    }
}
