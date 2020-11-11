using Newtonsoft.Json;
using PixivWallpaperHelper.Pixiv.Objects;
using PixivWallpaperHelper.Pixiv.Util;
using System.Threading.Tasks;

namespace PixivWallpaperHelper.Pixiv.Mode
{
    public class Fallback
    {
        private static readonly string API = "https://app-api.pixiv.net/v1/walkthrough/illusts";
        public static async Task<IllustList> GetFallback()
        {
            string json = await (await Request.CreateRequest(MethodType.GET, API)).GetResponseStringAsync();
            IllustList fallback = JsonConvert.DeserializeObject<IllustList>(json, Converter.Settings);

            return fallback;
        }
    }
}
